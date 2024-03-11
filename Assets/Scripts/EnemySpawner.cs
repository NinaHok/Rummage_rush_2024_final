using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    /* ENEMY SPAWNER
     * Spawns a wave of enemies to a chosen path 
     */

    // == PATHS ==
    [SerializeField] EnemyPath enemyPathA;
    [SerializeField] EnemyPath enemyPathB;

    // == ENEMIES ==
    [SerializeField] Enemy enemyDefault;
    [SerializeField] Enemy enemyFast;
    [SerializeField] Enemy enemyHeavy;


    private void SpawnEnemy(Enemy enemyToSpawn, EnemyPath chosenPath)
    {
        // which enemy to spawn, where, what rotation, which path
        Instantiate(enemyToSpawn, transform.position, Quaternion.identity).SetEnemyPath(chosenPath);
    }

    private void Awake()
    {
        StartCoroutine(Wave01()); // start wave 01 when the game starts 
    }


    IEnumerator Wave01()
    {
        yield return new WaitForSeconds(2);     // wait for 2 seconds
        SpawnEnemy(enemyDefault, enemyPathA);    // then spawn

        yield return new WaitForSeconds(2);     // wait for 2 seconds
        SpawnEnemy(enemyDefault, enemyPathA);    // then spawn

        yield return new WaitForSeconds(2);     // wait for 2 seconds
        SpawnEnemy(enemyFast, enemyPathB);       // then spawn

        yield return new WaitForSeconds(0.5f);  // wait for 0.5 seconds
        SpawnEnemy(enemyDefault, enemyPathA);    // then spawn

        yield return new WaitForSeconds(2);     // wait for 2 seconds
        SpawnEnemy(enemyDefault, enemyPathA);    // then spawn

        yield return new WaitForSeconds(4);     // wait for 2 seconds
        SpawnEnemy(enemyHeavy, enemyPathA);      // then spawn

        yield return new WaitForSeconds(2);     // wait for 2 seconds
        SpawnEnemy(enemyHeavy, enemyPathB);      // then spawn

    }

}
