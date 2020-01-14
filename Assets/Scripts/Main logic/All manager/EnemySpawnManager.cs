using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public UIManager uIManager;
    public GameController gameController;

    public float Score;
    private float TimeSpawn;

    private int WaveSize;
    private int CurrentNumberEnemies;
    private int NumberSpawn = 0;

    private bool SwitchSpawn;

    public GameObject SpawnPointEnemy;
    public GameObject PreEnemy;

    public ScriptableObjectsLevelListEnemy[] EnemyLevels;
    [SerializeField]
    private ScriptableObjectsEnemy[] SelectedUnits;

    private List<float> LevelsDistribution = new List<float>();

    [HideInInspector]
    public List<GameObject> EnemyList;

    private void Start()
    {
        WaveSize = Random.Range(10, 15);
        Score = 0;
        NextWave();
    }

    public void NextWave()
    {
        if (SwitchSpawn)
        {
            SwitchSpawn = false;
            WaveSize += Random.Range(4, 7);
            StartEnemySpawn();
        }
    }

    private void UpdateListLevelsDistribution()
    {
        LevelsDistribution.Clear();
        for (int index = 0; index < EnemyLevels.Length; index++)
        {
            LevelsDistribution.Add(EnemyLevels[index].Distribution.Evaluate(Score));
        }
    }

    public void StartEnemySpawn()
    {
        UpdateListLevelsDistribution();
        SelectedUnits = new ScriptableObjectsEnemy[WaveSize];
        CurrentNumberEnemies = WaveSize;
        UnitSelection();
        uIManager.AssignValuesEnemyCountText(WaveSize.ToString(), WaveSize.ToString());
        StartCoroutine(EnemySpawn());
    }

    private void UnitSelection()
    {
        for (int mainIndex = 0; mainIndex < SelectedUnits.Length; mainIndex++)
        {
            float value = Random.Range(0, LevelsDistribution.Sum());
            float sum = 0;
            if (Score < 300)
            {
                for (int addIndex = 0; addIndex < LevelsDistribution.Count; addIndex++)
                {
                    sum += LevelsDistribution[addIndex];
                    if (value < sum)
                    {
                        SelectedUnits[mainIndex] = EnemyLevels[addIndex].EnemyList[Random.Range(0, EnemyLevels[addIndex].EnemyList.Length)];
                        break;
                    }
                }
            }
            else SelectedUnits[mainIndex] = EnemyLevels[EnemyLevels.Length - 1].EnemyList[Random.Range(0, EnemyLevels[EnemyLevels.Length - 1].EnemyList.Length - 1)];
        }
    }

    private IEnumerator EnemySpawn()
    {
        for (int index = 0; index < SelectedUnits.Length; index++)
        {
            Spawn(SelectedUnits[index]);
            if (NumberSpawn < 41) TimeSpawn = FindOutSpawnTime();
            else TimeSpawn = Random.Range(0.5f, 1f);
            yield return new WaitForSeconds(TimeSpawn);
        }
        SelectedUnits = null;
        SwitchSpawn = true;
    }

    private float FindOutSpawnTime()
    {
        if (NumberSpawn <= 10) return Random.Range(1.5f, 4f);
        if (NumberSpawn > 10 && NumberSpawn <= 15) return Random.Range(1.5f, 3.5f);
        if (NumberSpawn > 15 && NumberSpawn <= 25) return Random.Range(1f, 2.5f);
        if (NumberSpawn > 25 && NumberSpawn <= 30) return Random.Range(0.5f, 2f);
        if (NumberSpawn > 30 && NumberSpawn <= 40) return Random.Range(0.5f, 1.5f);
        return 1f;
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

    public void UnRegisterEnemy(GameObject enemy, int score)
    {
        EnemyList.Remove(enemy);
        Destroy(enemy.gameObject);
        Score += score;
        CurrentNumberEnemies--;
        uIManager.AssignValuesEnemyCountText(CurrentNumberEnemies.ToString(), WaveSize.ToString());
        CheckForRemainingEnemies();
        Finish();
    }

    private void Finish()
    {
        gameController.EndGame();
    }

    private void CheckForRemainingEnemies()
    {
        if (CurrentNumberEnemies <= 0)
        {
            uIManager.SwitchPanelNextWave(true);
            // NextWave();
            // gameController.EndWave();
        }
    }

    public void AllStop()
    {
        StopAllCoroutines();
        for (int index = 0; index < EnemyList.Count; index++)
        {
            EnemyList[index].GetComponent<Enemy>().IsDead = true;
        }
    }
}