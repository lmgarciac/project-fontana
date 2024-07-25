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

    bool attacking = false;
    bool readyToAttack = true;
    int attackCount;

    PlayerControls playerControls;
    AudioSource audioSource;

    public void Attack()
    {
        if (!readyToAttack || attacking) return;

        readyToAttack = false;
        attacking = true;

        Invoke(nameof(ResetAttack), attackDelay);
        Invoke(nameof(AttackRaycast), attackSpeed);

        audioSource.pitch = Random.Range(0.95f, 1.15f);
        audioSource.PlayOneShot(brushSwing);
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

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playerControls.Player.Attack.WasPressedThisFrame())
        {
            Attack();
        }
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
