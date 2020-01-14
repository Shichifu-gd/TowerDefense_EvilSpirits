using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PathGenerationForOpponents pathGenerationForOpponents;
    public UIManager uIManager;
    public EnemySpawnManager enemySpawnManager;
    public Player player;

    private int Wave;
    private int CurrentWave;

    private void Start()
    {
        Wave = Random.Range(10, 20);
        CurrentWave = 0;
        EndWave();
    }

    public void EndGame()
    {
        if (player.Life <= 0)
        {
            uIManager.SwitchPanelRestart();
            enemySpawnManager.AllStop();
        }
    }

    public void EndWave()
    {
        CurrentWave++;
        uIManager.AssignValuesWaveText(CurrentWave.ToString(), Wave.ToString());
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}