using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homebase : MonoBehaviour
{


    [SerializeField] float homebaseHealth = 100f;
    private float currentHomebaseHealth;



    [SerializeField] HomebaseHealthBar healthBar;


    //Timers
    [SerializeField] float damageTakingTimer;
    [SerializeField] float damageTakingDelay = 2f;

    //Enemy bookkeping
    [SerializeField] LayerMask enemyLayers;
    [SerializeField] Collider[] colliders;
    [SerializeField] List<Enemy> enemiesInRange;
    [SerializeField] Enemy targetedEnemy;

    [SerializeField] Enemy enemyDefault;
    [SerializeField] Enemy enemyFast;
    [SerializeField] Enemy enemyHeavy;

    [SerializeField] float enemyDefaultDamage = 5f;
    [SerializeField] float enemyFastDamage = 2f;
    [SerializeField] float enemyHeavyDamage = 10f;

    // Start is called before the first frame update
    void Start()
    {
        currentHomebaseHealth = homebaseHealth;
    }

    // Update is called once per frame
    void Update()
    {
        ScanForEnemies();

        foreach(Enemy enemyDefault in enemiesInRange)
        {
            damageTakingTimer += Time.deltaTime;
            if (damageTakingTimer >= damageTakingDelay)
            {
                damageTakingTimer = 0;
                TakeDamage(enemyDefaultDamage);
            }
        }

        foreach(Enemy enemyFast in enemiesInRange)
        {
            damageTakingTimer += Time.deltaTime;
            if (damageTakingTimer >= damageTakingDelay)
            {
                damageTakingTimer = 0;
                TakeDamage(enemyFastDamage);
            }
        }

        foreach (Enemy enemyHeavy in enemiesInRange)
        {
            damageTakingTimer += Time.deltaTime;
            if (damageTakingTimer >= damageTakingDelay)
            {
                damageTakingTimer = 0;
                TakeDamage(enemyHeavyDamage);
            }
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

    public void TakeDamage(float enemyDamage)
    {
        if (targetedEnemy != null)
        {
           currentHomebaseHealth -= enemyDamage * enemiesInRange.Count;

           healthBar.UpdateHomebaseHealthBar(currentHomebaseHealth, homebaseHealth);


            if(currentHomebaseHealth <= 0)
            {
                Debug.Log($"Health = 0. Game over");
            }

        }
    }

}
