using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Details")]
    public float attackDistance = 3f;
    public float attackDelay = 0.3f;
    public float attackSpeed = 0.3f;
    public float attackDamage = 15f; // For later combination with colour mixing
    public LayerMask attackLayer;

    [Header("Impact Frame Details")]
    public float impactFrameDelay = 0.2f;
    public float impactFrameTimeScale = 0.2f;

    [Header("Miscellaneous Details")]
    public GameObject hitEffect;
    public AudioClip brushSwing;
    public AudioClip hitSound;

    [SerializeField]
    private Transform playerLineOfSight; //This can go into PlayerInteractions too

    [SerializeField]
    Animator handsAnimator;

    float elapsedFrameTime = 0f;
    bool enemyHit = false;
    bool attacking = false;
    bool readyToAttack = true;
    int attackCount;

    //public const string IDLE = "Idle"; THESE ARE UNUSED STILL, COULD IMPLEMENT IN ANOTHER SCRIPT
    //public const string WALK = "Walk";
    public const string ATTACK1 = "Attack 1";
    public const string ATTACK2 = "Attack 2";

    string currentAnimationState;

    
    PlayerControls playerControls;
    AudioSource audioSource;

    private void Awake()
    {
        playerControls = new PlayerControls();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playerControls.Player.Attack.WasPressedThisFrame())
        {
            Attack();
        }

        CheckImpactFrame();
    }

    void CheckImpactFrame()
    {
        if (enemyHit)
        {
            elapsedFrameTime += Time.unscaledDeltaTime;
            if (elapsedFrameTime >= impactFrameDelay)
            {
                Time.timeScale = 1f; // Reset time scale
                enemyHit = false;
                elapsedFrameTime = 0f;
            }
        }
        
    }

    void StartImpactFrame()
    {
        enemyHit = true;
        Time.timeScale = impactFrameTimeScale;
    }

    public void Attack()
    {
        if (!readyToAttack || attacking) return;

        readyToAttack = false;
        attacking = true;

        Invoke(nameof(ResetAttack), attackDelay);
        Invoke(nameof(AttackRaycast), attackSpeed);

        audioSource.pitch = Random.Range(0.95f, 1.15f);
        audioSource.PlayOneShot(brushSwing);

        if (attackCount == 0) // Two different types of attack
        {
            ChangeAnimationState(ATTACK1);
            attackCount++;
        }
        else
        {
            ChangeAnimationState(ATTACK2);
            attackCount = 0;
        }
    }

    void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;
    }

    private void HitTarget(Vector3 point)
    {
        audioSource.pitch = 1;
        audioSource.PlayOneShot(hitSound);

        // This is disabled until URP is set on the project
        //GameObject GO = Instantiate(hitEffect, point, Quaternion.identity);
        //Destroy(GO,2f); //Maybe there's a better way to do this without creating objects
    }

    void AttackRaycast()
    {
        if(Physics.Raycast(playerLineOfSight.position, playerLineOfSight.forward, out RaycastHit hit, attackDistance, attackLayer))
        {
            if(hit.transform.TryGetComponent<EnemyTest>(out EnemyTest enemy)){
                StartImpactFrame(); //Check this later
                enemy.DamageHealth(attackDamage, GlobalManager.Instance.CurrentColorType);
            }
            else if (hit.transform.TryGetComponent<CriticalSpot>(out CriticalSpot critSpot))
            {
                critSpot.CritDamage(attackDamage, GlobalManager.Instance.CurrentColorType);
            }

            HitTarget(hit.point);
        }
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentAnimationState == newState) return;

        currentAnimationState = newState;
        handsAnimator.CrossFadeInFixedTime(currentAnimationState, 0.2f);
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}
