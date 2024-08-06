using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    private bool collided;
    private Vector3 destinationPoint;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = destinationPoint * projectileSpeed;
    }

    public void SetProjectileDestination(Vector3 destinationVector)
    {
        destinationPoint = destinationVector;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile") && collision.gameObject.CompareTag("Player") && !collided)
        {
            //Add attack here
            collided = true; //To limit to first collision
            Destroy(gameObject);
        }
    }
}
