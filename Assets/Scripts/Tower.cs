using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    // Enemyslots
    // Positions for enemies around the towers
    [SerializeField] List<EnemySlot> enemySlots;

    //default values or reset values
    [SerializeField] public float defaultFiringDelay;

    [SerializeField] public float towerCost = 25f;
    [SerializeField] public float towerHealth = 80f;
    public float currentTowerHealth;

    [SerializeField] float range = 3.0f;
    [SerializeField] Projectile projectile;
    [SerializeField] Transform firingPoint;
    [SerializeField] Transform targetPoint;

    public bool towerIsActive;

    // Timers
    [SerializeField] float firingTimer;
    [SerializeField] public float firingDelay = 1.0f;

    float scanningTimer;
    float scanningDelay = 0.1f;

    // Enemy bookkeeping
    [SerializeField] LayerMask enemyLayers;
    [SerializeField] Collider[] colliders;
    [SerializeField] List<Enemy> enemiesInRange;
    [SerializeField] Enemy targetedEnemy;

    [SerializeField] TowerHealthBar towerHealthBar;

    [SerializeField] GameSettingsSO gameSettings;

    private void Awake()
    {
        // initial setup
        towerIsActive = false;
        currentTowerHealth = towerHealth;
        firingDelay = defaultFiringDelay;

        // populate enemyslots
        enemySlots = new List<EnemySlot>();
        foreach (Transform t in transform)
            if (t.name == "EnemySlots")
                foreach (Transform slot in t)
                    enemySlots.Add(new EnemySlot(slot));

    }


    private void Update()
    {
        if (gameSettings.currentGameState == GameStates.inGame)
        {

            if (towerIsActive)
            {
                towerHealthBar.UpdateTowerHealthBar(currentTowerHealth, towerHealth);

                // == SCANNING PART ==

                scanningTimer += Time.deltaTime;
                if (scanningTimer >= scanningDelay)
                {
                    scanningTimer = 0;   // reset scanning timer
                    ScanForEnemies();    // call the scan function
                }


                // == FIRING PART ==

                // if there's a targeted enemy, then increment the timer every frame
                if (targetedEnemy)
                    firingTimer += Time.deltaTime;

                // if we have reached the firingDelay, then reset the timer and fire
                if (firingTimer >= firingDelay)
                {
                    firingTimer = 0f;
                    Fire();            // call the fire function

                }
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

    public void TakeDamage(float enemyDamage)
    {
        {
            currentTowerHealth -= enemyDamage;

            if (currentTowerHealth <= 0)
            {
                Destroy(this.gameObject);
            }

        }
    }

    public Transform GetTargetPoint()
    {
        return targetPoint;
    }

    public bool GetEnemySlot(Enemy enemy, out Transform transform)
    {
        // This method allows the tower to tell an
        // attacking enemy where to go and stand

        // Is the enemy already occupying a slot?
        foreach (EnemySlot slot in enemySlots)
        {
            if (slot.enemy == enemy)
            {
                transform = slot.transform;
                return true;
            }
        }

        // If not, is there an emptly slot available?
        for(int i = 0; i < enemySlots.Count; i++)
        {
            if (enemySlots[i].enemy == null)
            {
                Debug.Log($"Assigning a new slot for enemy {enemy.name}");
                enemySlots[i].SetEnemy(enemy);
                transform = enemySlots[i].transform;
                return true;
            }
        }

        // No slots available
        transform = null;
        return false;
    }


    
    [Serializable] class EnemySlot
    {
        // This class stores 2 things:
        // - a transform (empty) around the tower
        // - an attacking enemy

        public Transform transform;
        public Enemy enemy;

        public EnemySlot(Transform transform)
        {
            this.transform = transform;
            this.enemy = null;
        }

        public void SetEnemy(Enemy enemy)
        {
            this.enemy = enemy;
        }
    }

}



