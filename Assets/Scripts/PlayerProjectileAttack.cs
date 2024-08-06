using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileAttack : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;

    private Vector3 projectileDestination;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            FireProjectile();
        }
    }

    private void FireProjectile()
    {
        Ray ray = mainCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit rayHit;

        if(Physics.Raycast(ray, out rayHit))
        {
            projectileDestination = rayHit.point;
        }
        else
        {
            projectileDestination = ray.GetPoint(1000); //This could be serializable
        }

        InstantiateProjectile(projectileSpawnPoint);
    }

    private void InstantiateProjectile(Transform spawnPoint)
    {
        GameObject projectileObj = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        projectileObj.GetComponent<ProjectileLogic>().SetProjectileDestination((projectileDestination - spawnPoint.position).normalized);
    }
}
