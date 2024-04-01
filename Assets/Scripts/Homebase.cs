using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homebase : MonoBehaviour
{


    [SerializeField] float homebaseHealth = 20f;
    private float currentHealth;
    [SerializeField] float incomingEnemyDamage = 10f;

    
    [SerializeField] HomebaseHealthBar healthBar;


    //Timers
    [SerializeField] float damageTakingTimer;
    [SerializeField] float damageTakingDelay;

    //Enemy bookkeping
    [SerializeField] LayerMask enemyLayers;
    [SerializeField] Collider[] colliders;
    [SerializeField] List<Enemy> enemiesInRange;
    [SerializeField] Enemy targetedEnemy;
   

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = homebaseHealth;
    }

    // Update is called once per frame
    void Update()
    {       
        ScanForEnemies();

        if (targetedEnemy)
        {
            damageTakingTimer += Time.deltaTime;

        }

        if(damageTakingTimer >= damageTakingDelay)
        {
            TakeDamage();
            damageTakingTimer = 0;
            
        }
    }


    private void ScanForEnemies()
    {
        colliders = Physics.OverlapBox(transform.position, transform.localScale/2, transform.rotation, enemyLayers);

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

    public void TakeDamage()
    {
        if (targetedEnemy != null)
        {
            currentHealth -= incomingEnemyDamage;

            HomebaseHealthBar.UpdateHomebaseHealthBar(currentHealth, homebaseHealth);


            if(currentHealth <= 0)
            {
                Debug.Log($"Health = 0. Game over");
            }

        }
    }

}
