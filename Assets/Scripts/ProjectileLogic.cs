using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class ProjectileLogic : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileDamage;
    [SerializeField] private float projectileLifetime;

    private ColorType projectileColor;
    private bool collided;
    private Vector3 destinationPoint;
    private ParticleSystem beamParticles;

    public ColorType ProjectileColor { get => projectileColor; set => projectileColor = value; }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = destinationPoint * projectileSpeed;
        beamParticles = GetComponent<ParticleSystem>();
        ChangeBeamColor(GlobalManager.Instance.CurrentColorType); //Grabs color at the time of spawning, could result in some bugs. Check it later

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
            enemyToHit.DamageHealth(projectileDamage, ProjectileColor);

            Destroy(gameObject);
        }

        //if (collision.gameObject.CompareTag("Projectile") && collision.gameObject.CompareTag("Player") && !collided)
        //{
        //    if(collision.gameObject.TryGetComponent<EnemyTest>(out EnemyTest enemyToHit))
        //    {
        //        enemyToHit.DamageHealth(projectileDamage, ProjectileColor);
        //    }

        //    collided = true; //To limit to first collision
        //    Destroy(gameObject);
        //}
    }

    void ChangeBeamColor(ColorType beamColor) //Bugged code, rework needed
    {
        //switch (beamColor) //There must be a better way to do this apart from this and a damage values matrix
        //{
        //    case ColorType.Red:
        //        beamParticles.startColor = Color.red; //I know this is obsolete, but its the only way I can make it work
        //        break;

        //    case ColorType.Yellow:
        //        beamParticles.startColor = Color.yellow;
        //        break;

        //    case ColorType.Blue:
        //        beamParticles.startColor = Color.blue;
        //        break;

        //    case ColorType.Orange:
        //        beamParticles.startColor = new Color(1, 0.2f, 0, 1); //Orange
        //        break;

        //    case ColorType.Purple:
        //        beamParticles.startColor = new Color(1, 0, 1, 1);
        //        break;

        //    case ColorType.Green:
        //        beamParticles.startColor = Color.green;
        //        break;

        //    case ColorType.White:
        //        beamParticles.startColor = Color.white;
        //        break;
        //}
    }


}
