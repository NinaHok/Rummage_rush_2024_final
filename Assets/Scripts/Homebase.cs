using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homebase : MonoBehaviour
{

    [SerializeField] EventManagerSO eventManager;


    [SerializeField] float homebaseHealth = 100f;
    public float currentHomebaseHealth;
    [SerializeField] HomebaseHealthBar homebaseHealthBar;


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



    // Start is called before the first frame update
    void Start()
    {
        currentHomebaseHealth = homebaseHealth;
    }

    // Update is called once per frame
    void Update()
    {
        ScanForEnemies();
        homebaseHealthBar.UpdateHomebaseHealthBar(currentHomebaseHealth, homebaseHealth);

        foreach(Enemy enemyDefault in enemiesInRange)
        {
            damageTakingTimer += Time.deltaTime;
            if (damageTakingTimer >= damageTakingDelay)
            {
                damageTakingTimer = 0;
                TakeDamage(enemyDefault.enemyDefaultDamage);
            }
        }

        foreach(Enemy enemyFast in enemiesInRange)
        {
            damageTakingTimer += Time.deltaTime;
            if (damageTakingTimer >= damageTakingDelay)
            {
                damageTakingTimer = 0;
                TakeDamage(enemyFast.enemyFastDamage);
            }
        }

        foreach (Enemy enemyHeavy in enemiesInRange)
        {
            damageTakingTimer += Time.deltaTime;
            if (damageTakingTimer >= damageTakingDelay)
            {
                damageTakingTimer = 0;
                TakeDamage(enemyHeavy.enemyHeavyDamage);
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
           currentHomebaseHealth -= enemyDamage;

            if(currentHomebaseHealth <= 0)
            {
                Debug.Log($"Health = 0. Game over");
                eventManager.GameOver();
            }

        }
    }

}
