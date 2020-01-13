using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject SpawnPointEnemy;

    public GameObject PreEnemy;
    public ScriptableObjectsEnemy[] EnemyLevelOne;
    public ScriptableObjectsEnemy[] EnemyLevelTwo;
    public ScriptableObjectsEnemy[] EnemyLevelThree;

    private int Wave;
    private int NumberEnemiesFirstLevel;
    private int NumberEnemiesSecondLevel;
    private int NumberEnemiesThirdLevel;

    public bool SwitchStart { get; set; }

    public List<GameObject> EnemyList;

    private void Update()
    {
        if (SwitchStart) StartCoroutine(EnemySpawn());
    }

    private IEnumerator EnemySpawn()
    {
        SwitchStart = false;
        int randomInndex = Random.Range(0, EnemyLevelOne.Length);
        yield return new WaitForSeconds(1f);
        Spawn(EnemyLevelOne[randomInndex]);
        SwitchStart = true;
    }

    private void Spawn(ScriptableObjectsEnemy enemy)
    {
        GameObject enemyObject = Instantiate(PreEnemy, SpawnPointEnemy.transform.position, Quaternion.identity);
        enemyObject.transform.parent = GameObject.FindGameObjectWithTag("EnemySlot").transform;
        enemyObject.GetComponent<Enemy>().AssignValuesForEnemy(enemy);
    }

    public void RegisterEnemy(GameObject enemy)
    {
        EnemyList.Add(enemy);
    }

    public void UnRegisterEnemy(GameObject enemy)
    {
        EnemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    public void DestroyAllEnemy()
    {
        foreach (GameObject enemy in EnemyList)
        {
            Destroy(enemy.gameObject);
        }
        EnemyList.Clear();
    }
}