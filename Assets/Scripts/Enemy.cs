using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //speed
    [SerializeField] float speed = 2.5f;

    //range
    float range = 2.0f;

    [SerializeField] Enemy enemyDefault;
    [SerializeField] Enemy enemyFast;
    [SerializeField] Enemy enemyHeavy;

    //damage
    [SerializeField] public float enemyDefaultDamage = 5f;
    [SerializeField] public float enemyFastDamage = 2f;
    [SerializeField] public float enemyHeavyDamage = 10f;

    //damage timers
    [SerializeField] float damageDealingTimer;
    [SerializeField] float damageDealingDelay = 2f;

    // get reference to the road
    [SerializeField] EnemyPath enemyPath;

    // hit target (where to hit the enemy)
    [SerializeField] Transform hitTarget;

    // health
    [SerializeField] float maxHealth = 10.0f;
    private float currentHealth;
    [SerializeField] HealthBar healthBar;

    //tower bookkeping
    [SerializeField] LayerMask towerLayers;
    [SerializeField] Collider[] colliders;
    [SerializeField] List<Tower> towersInRange;
    [SerializeField] Tower targetedTower;
    [SerializeField] TowerHealthBar towerHealthBar;


    // remember where to go
    private int currentTargetWaypoint = 0;

    private bool hasReachedEnd;

    private void Awake()
    {
        //set max health
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (!hasReachedEnd) // hasReachedEnd == false
        {
            // block comments:    ctrl + k, ctrl + c 
            // unblock comments:  ctlr + k, ctrl + u 

            ScanForTower();


            if (targetedTower != null && targetedTower.towerIsActive)
            {
                transform.LookAt(targetedTower.transform.position);
                transform.position = Vector3.MoveTowards(
                transform.position,                                    // where from
                targetedTower.transform.position,                      // where to
                speed * Time.deltaTime                                 // how fast
                );

                foreach (Collider towerDefault in colliders)
                {
                    damageDealingTimer += Time.deltaTime;
                    if (damageDealingTimer >= damageDealingDelay)
                    {
                        damageDealingTimer = 0;
                        targetedTower.TakeDamage(enemyDefaultDamage);


                    }

                }

                foreach (Collider towerFast in colliders)
                {
                    damageDealingTimer += Time.deltaTime;
                    if (damageDealingTimer >= damageDealingDelay)
                    {
                        damageDealingTimer = 0;
                        targetedTower.TakeDamage(enemyFastDamage);

                    }

                }

                foreach (Collider towerHeavy in colliders)
                {
                    damageDealingTimer += Time.deltaTime;
                    if (damageDealingTimer >= damageDealingDelay)
                    {
                        damageDealingTimer = 0;
                        targetedTower.TakeDamage(enemyHeavyDamage);

                    }

                }

            }



            {

                // look at the destination
                transform.LookAt(enemyPath.GetWaypoint(currentTargetWaypoint));

                // move to the destination
                transform.position = Vector3.MoveTowards(
                transform.position,                                    // where from
                enemyPath.GetWaypoint(currentTargetWaypoint).position, // where to
                speed * Time.deltaTime                                 // how fast
                );

                // are we close enough to the destination?
                if (Vector3.Distance(transform.position,
                enemyPath.GetWaypoint(currentTargetWaypoint).position) < 0.1f)
                {
                    // increment the current target waypoint
                    currentTargetWaypoint++;

                    // have we surpassed the last waypoint?
                    if (currentTargetWaypoint >= enemyPath.GetNumberOfWaypoints())
                    {
                        hasReachedEnd = true;  // have we reached the end of the road? 
                    }

                }
            }
        }
    }



    // let the enemy know which path to follow
    public void SetEnemyPath(EnemyPath incomingPath)
    {
        enemyPath = incomingPath;
    }

    public void InflictDamage(float incomingDamage)
    {

        currentHealth -= incomingDamage;

        // update the healthbar
        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        // gg wp
        if (currentHealth <= 0)
        {
            // TODO: Reward the player

            Destroy(this.gameObject);
        }
    }

    public Transform getHitTarget()
    {
        return hitTarget;
    }

    public void ScanForTower()
    {
        colliders = Physics.OverlapSphere(transform.position, range, towerLayers);

        towersInRange.Clear();

        foreach (Collider collider in colliders)
        {
            towersInRange.Add(collider.GetComponent<Tower>());
        }

        if (towersInRange.Count != 0)
        {
            targetedTower = towersInRange[0];
        }


    }

}