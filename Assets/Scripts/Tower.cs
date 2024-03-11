using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    [SerializeField] float range = 3.0f;
    [SerializeField] Projectile projectile;
    [SerializeField] Transform firingPoint;

    private bool towerIsActive;

    // Timers
    [SerializeField] float firingTimer;
    [SerializeField]float firingDelay = 1.0f;

    float scanningTimer;
    float scanningDelay = 0.1f;

    // Enemy bookkeeping
    [SerializeField] LayerMask enemyLayers;
    [SerializeField] Collider[] colliders;
    [SerializeField] List<Enemy> enemiesInRange;
    [SerializeField] Enemy targetedEnemy;

    private void Awake()
    {
        // initial setup
        towerIsActive = false;
    }


    private void Update()
    {
        
        if (towerIsActive)
        {
        
          // == SCANNING PART ==

          scanningTimer += Time.deltaTime;
          if (scanningTimer >= scanningDelay)
          {
            scanningTimer = 0;   // reset scanning timer
            ScanForEnemies();    // call the scan function
          }


          // == FIRING PART ==

          // if there's a targeted enemy, then increment the timer every frame
          if(targetedEnemy)
            firingTimer += Time.deltaTime;

          // if we have reached the firingDelay, then reset the timer and fire
          if(firingTimer >= firingDelay)
          {
            firingTimer = 0f;
            Fire();            // call the fire function
          }

        }
    }


    private void ScanForEnemies()
    {
        // Find the surrounding colliders, only detect objects on enemy layer
        colliders = Physics.OverlapSphere(transform.position, range, enemyLayers);

        // Clear the list first
        enemiesInRange.Clear();

        // Go over each of the detected colliders
        foreach (Collider collider in colliders)
        {
            enemiesInRange.Add(collider.GetComponent<Enemy>());
        }

        // If there are enemies in range, pick one to target
        if (enemiesInRange.Count != 0)
        {
            targetedEnemy = enemiesInRange[0];
        }

    }

    private void Fire()
    {
        // make sure there is something to shoot at
        if (targetedEnemy != null)
        {
            // get enemy direction relative to the tower
            Vector3 enemyDirection
                = (targetedEnemy.transform.position - firingPoint.position).normalized;


            // create and setup a projectile
            Instantiate(projectile, firingPoint.position, Quaternion.identity)
                .Setup(enemyDirection, targetedEnemy);
        }

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void activateTower()
    {
        towerIsActive = true;
    }


}

