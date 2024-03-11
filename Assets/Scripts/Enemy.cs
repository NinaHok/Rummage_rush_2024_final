using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    
    [SerializeField] float speed = 5.0f;

    // get reference to the road
    [SerializeField] EnemyPath enemyPath;

    // hit target (where to hit the enemy)
    [SerializeField] Transform hitTarget;

    // health
    [SerializeField] float maxHealth = 10.0f;
    private float currentHealth;
    [SerializeField] HealthBar healthBar;

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
                enemyPath.GetWaypoint(currentTargetWaypoint).position) < 0.2f)
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

    // let the enemy know which path to follow
    public void SetEnemyPath(EnemyPath incomingPath)
    {
        enemyPath = incomingPath;
    }

    public void InflictDamage(float incomingDamage)
    {
        Debug.Log($"About to take damage: {currentHealth} - {incomingDamage}");

        currentHealth -= incomingDamage;

        // update the healthbar
        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        // gg wp
        if(currentHealth <= 0)
        {
            // TODO: Reward the player
            
            Destroy(this.gameObject);
        }
    }

    public Transform getHitTarget()
    {
        return hitTarget;
    }


}
