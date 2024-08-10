using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class ProjectileLogic : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileDamage;
    [SerializeField] private float projectileLifetime;
    [SerializeField] private ColorType projectileColor;

    private Vector3 destinationPoint;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = destinationPoint * projectileSpeed;
        Destroy(this.gameObject, projectileLifetime);
    }

    public void SetProjectileDestination(Vector3 destinationVector)
    {
        destinationPoint = destinationVector;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.TryGetComponent<EnemyTest>(out EnemyTest enemyToHit);
            enemyToHit.DamageHealth(projectileDamage, projectileColor);

            Destroy(gameObject);
        }
    }
}
