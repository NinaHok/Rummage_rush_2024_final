using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homebase : MonoBehaviour
{


    [SerializeField] float homebaseHealth = 20f;
    private float currentHealth;
    
    [SerializeField] HealthBar healthBar;
    [SerializeField] float range = 3f;


    //Timers
    [SerializeField] float damageTakingTimer;
    [SerializeField] float damageTakingDelay;

    //Enemy bookkeping
    [SerializeField] LayerMask enemyLayers;
    [SerializeField] Collider[] colliders;
    [SerializeField] List<Enemy> enemiesInRange;
    [SerializeField] Enemy targetedEnemy;
    private float incomingDamage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesInRange.Count > 0)
        {
            damageTakingTimer += Time.deltaTime;
            if (damageTakingTimer >= damageTakingDelay)
            {
                damageTakingDelay = 0f;
                ScanForEnemies();
            }
        }
    }


    private void ScanForEnemies()
    {
        colliders = Physics.OverlapSphere(transform.position, range, enemyLayers);

        enemiesInRange.Clear();

        foreach (Collider collider in colliders)
        {
            enemiesInRange.Add(collider.GetComponent<Enemy>());
        }

        if (enemiesInRange.Count !=0)
        {
            targetedEnemy = enemiesInRange[0];
        }
    }

    private void TakeDamage(float incomingDamage)
    {
        if (targetedEnemy != null)
        {
            currentHealth -= incomingDamage;

            healthBar.UpdateHealthBar(currentHealth, homebaseHealth);

            if(currentHealth <= 0)
            {
                Debug.Log($"Health = 0. Game over");
            }

        }
    }

}
