using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class Enemy : MonoBehaviour
{

    // Enemy states
    private enum EnemyState
    {
        Stopped,
        Traveling,
        MovingToAttack,
        Attacking
    }

    private EnemyState enemyState;

    private Transform enemySlotAroundTower;


    //default values or reset values
    [SerializeField] public float defaultSpeed;

    [SerializeField] public float defaultHealth;



    //speed
    [SerializeField] public float speed;

    //range
    float range = 2.0f;

    //damage
    [SerializeField] public float enemyDamage = 5f;

    //drop reward
    [SerializeField] float rewardCost = 30f;
 
    //damage timers
    [SerializeField] float damageDealingTimer;
    [SerializeField] float damageDealingDelay = 2f;

    // get reference to the road
    [SerializeField] EnemyPath enemyPath;

    // hit target (where to hit the enemy)
    [SerializeField] Transform hitTarget;

    // health
    [SerializeField] public float maxHealth;
    [SerializeField] public float currentHealth;
    [SerializeField] HealthBar healthBar;

    //tower bookkeping
    [SerializeField] LayerMask towerLayers;
    [SerializeField] Collider[] colliders;
    [SerializeField] List<Tower> towersInRange;
    [SerializeField] Tower targetedTower;
    [SerializeField] TowerHealthBar towerHealthBar;

    [SerializeField] GameSettingsSO gameSettings;
    [SerializeField] EventManagerSO eventManager;



    // remember where to go
    private int currentTargetWaypoint = 0;

    private void Awake()
    {
        //set max health
        maxHealth = defaultHealth;
        currentHealth = maxHealth;
        speed = defaultSpeed;

        enemyState = EnemyState.Traveling;
    }

    private void Update()
    {
        if (gameSettings.currentGameState == GameStates.inGame)
        {
            // Choose enemy behaviour based on the current state
            switch (enemyState)
            {
                // = Stopped =
                // Enemy has already reached the end of path
                // Nothing to do but scan for towers
                case EnemyState.Stopped:
                {
                    ScanForTower();
                    if (targetedTower != null && targetedTower.towerIsActive)
                        enemyState = EnemyState.MovingToAttack;
                    break;
                }

                // = Regular traveling =
                // Enemy is traveling along points
                case EnemyState.Traveling:
                {
                    // have we surpassed the last waypoint?
                    if (currentTargetWaypoint >= enemyPath.GetNumberOfWaypoints())
                    {
                        // Reached the end, stop moving
                        enemyState = EnemyState.Stopped; break;
                    }

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
                    }

                    ScanForTower();
                    if (targetedTower != null && targetedTower.towerIsActive)
                        enemyState = EnemyState.MovingToAttack;
                    break;
                }

                // = Moving to attack a tower =
                // Enemy has detected a tower and moves into an
                // available slot, if possible
                case EnemyState.MovingToAttack:
                {
                    if (targetedTower != null && targetedTower.towerIsActive)
                    {
                        if (!enemySlotAroundTower)
                        {
                            if(targetedTower.GetEnemySlot(this, out Transform slotTransform))
                            {
                                enemySlotAroundTower = slotTransform;
                            }
                            else
                            {
                                // Cannot find a free slot around the tower
                                // Getting out of here
                                enemyState = EnemyState.Traveling; break;
                            }
                        }


                        transform.LookAt(enemySlotAroundTower.transform.position);
                        transform.position = Vector3.MoveTowards(
                        transform.position,                                    // where from
                        enemySlotAroundTower.transform.position,               // where to
                        speed * Time.deltaTime                                 // how fast
                        );

                        if (Vector3.Distance(transform.position,
                            enemySlotAroundTower.transform.position) < 0.1f)
                        {
                            enemyState = EnemyState.Attacking; break;
                        }
                        break;
                    }

                    // Nothing to do, go back to travelling
                    enemyState = EnemyState.Traveling; break;
                }

                // = Attacking a tower =
                case EnemyState.Attacking:
                {
                    if (targetedTower != null && targetedTower.towerIsActive)
                    {
                        foreach (Collider tower in colliders)
                        {
                            damageDealingTimer += Time.deltaTime;
                            if (damageDealingTimer >= damageDealingDelay)
                            {
                                damageDealingTimer = 0;
                                targetedTower.TakeDamage(enemyDamage);
                            }
                        }
                        break;
                    }

                    // Nothing to do, go back to travelling
                    enemyState = EnemyState.Traveling; break;
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
        gameSettings.damageDealt += incomingDamage;

        // update the healthbar
        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        // gg wp
        if (currentHealth <= 0)
        {
            // TODO: Reward the player
            gameSettings.money += rewardCost;
            eventManager.EnemyDestroyed();
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