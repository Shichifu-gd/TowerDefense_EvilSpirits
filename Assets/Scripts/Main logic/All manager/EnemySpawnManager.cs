using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject SpawnPointEnemy;

    public GameObject[] EnemyLevelOne;
    public GameObject[] EnemyLevelTwo;
    public GameObject[] EnemyLevelThree;

    private int Wave;
    private int NumberEnemiesFirstLevel;
    private int NumberEnemiesSecondLevel;
    private int NumberEnemiesThirdLevel;

    public bool SwitchStart { get; set; }

    public List<Enemy> EnemyList;

    private void Update()
    {
        if (SwitchStart) StartCoroutine(EnemySpawn());
    }

    private void EnemyChoice()
    {

    }

    private IEnumerator EnemySpawn()
    {
        SwitchStart = false;
        yield return new WaitForSeconds(1f);
        Spawn(EnemyLevelOne[0]);
        SwitchStart = true;
    }

    private void Spawn(GameObject enemy)
    {
        GameObject enemyObject = Instantiate(enemy, SpawnPointEnemy.transform.position, Quaternion.identity);
        enemyObject.transform.parent = GameObject.FindGameObjectWithTag("EnemySlot").transform;
    }

    public void RegisterEnemy(Enemy enemy)
    {
        EnemyList.Add(enemy);
    }

    public void UnRegisterEnemy(Enemy enemy)
    {
        EnemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    public void DestroyAllEnemy()
    {
        foreach (Enemy enemy in EnemyList)
        {
            Destroy(enemy.gameObject);
        }
        EnemyList.Clear();
    }
}