using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaBlaster : MonoBehaviour
{
    // Spawner Variables
    public GameObject projectilePrefab;
    public float shootStartDelay;
    public float timeBetweenShots;

    // Projectile Variables
    public float shootSpeed;
    public bool goingLeft;
    private bool useBigBullet;
    public GameObject Bullet;

    void Start()
    {
        Shoot();
        InvokeRepeating("Shoot", shootStartDelay, timeBetweenShots);
    }

    void SpawnProjectile()
    {
        // If Mega Man pickes up a BigBullet upgrade
        if (!useBigBullet)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
            if (projectile.GetComponent<Laser>())
            {
                projectile.GetComponent<Laser>().goingLeft = goingLeft;
            }
        }

        // Regular laser
        else
        {
            GameObject projectile = Instantiate(Bullet, transform.position, projectilePrefab.transform.rotation);
            if (projectile.GetComponent<Laser>())
            {
                projectile.GetComponent<Laser>().goingLeft = goingLeft;
            }
        }
    }

    // Fire button
    void Shoot()
    {
        if (Input.GetKey("space"))
        {
            SpawnProjectile();
        }
    }

    //Activates BigBullet
    public void ChangeBullet()
    {
        useBigBullet = true;
    }
}
