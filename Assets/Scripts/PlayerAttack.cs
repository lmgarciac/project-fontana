using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public float attackDistance = 3f;
    public float attackDelay = 0.1f;
    public float attackSpeed = 0.3f;
    public int attackDamage = 1; // For later combination with colour mixing
    public LayerMask attackLayer;

    public GameObject hitEffect;
    public AudioClip brushSwing;
    public AudioClip hitSound;

    [SerializeField]
    private Transform playerLineOfSight; //This can go into PlayerInteractions too

    [SerializeField]
    Animator handsAnimator;

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

        GameObject GO = Instantiate(hitEffect, point, Quaternion.identity);
        Destroy(GO,2f); //Maybe there's a better way to do this without creating objects

        Debug.Log("HIT ON TARGET");
    }

    void AttackRaycast()
    {
        if(Physics.Raycast(playerLineOfSight.position, playerLineOfSight.forward, out RaycastHit hit, attackDistance, attackLayer))
        {
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
