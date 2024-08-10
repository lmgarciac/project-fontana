using Utils;
using UnityEngine;
using System;

public class PlayerProjectileAttack : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private GameObject[] projectilePrefabs;
    [SerializeField] private Transform projectileSpawnPoint;

    private Vector3 projectileDestination;
    private GameObject projectilePrefab;

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
        CheckCurrentColor();
        GameObject projectileObj = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);

        ProjectileLogic projectileSpawned = projectileObj.GetComponent<ProjectileLogic>();
        projectileSpawned.SetProjectileDestination((projectileDestination - spawnPoint.position).normalized);
    }

    private void CheckCurrentColor()
    {
        switch (GlobalManager.Instance.CurrentColorType) //There must be a better way to do this but I'm falling asleep rn
        {
            case ColorType.Red:
                projectilePrefab = projectilePrefabs[0];
                break;

            case ColorType.Yellow:
                projectilePrefab = projectilePrefabs[1];
                break;

            case ColorType.Blue:
                projectilePrefab = projectilePrefabs[2];
                break;

            case ColorType.Orange:
                projectilePrefab = projectilePrefabs[3];
                break;

            case ColorType.Purple:
                projectilePrefab = projectilePrefabs[4];
                break;

            case ColorType.Green:
                projectilePrefab = projectilePrefabs[5];
                break;

            case ColorType.White:
                projectilePrefab = projectilePrefabs[6];
                break;
        }
    }
}
