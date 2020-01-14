using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Text WaveText;
    public Text GoldText;
    public Text LifeText;
    public Text EnemyCountText;

    public GameObject PanelMain;
    public GameObject PanelRestart;
    public GameObject PanelNextWave;
    public GameObject PanelShopTower;

    private void Start()
    {
        PanelMain.SetActive(false);
        PanelRestart.SetActive(false);
        PanelNextWave.SetActive(false);
        PanelShopTower.SetActive(false);
    }

    public void AssignValuesGoldText(string gold)
    {
        GoldText.text = gold;
    }

    public void AssignValuesLifeText(string life)
    {
        LifeText.text = life;
    }

    public void AssignValuesEnemyCountText(string current, string max)
    {
        EnemyCountText.text = $"{current} / {max}";
    }

    public void AssignValuesWaveText(string current, string max)
    {
        WaveText.text = $"{current} / {max}";
    }

    public void OnPanel()
    {
        PanelMain.SetActive(true);
        PanelShopTower.SetActive(true);
    }

    public void SwitchPanelNextWave(bool option)
    {
        PanelNextWave.SetActive(option);
    }

    public void SwitchPanelRestart()
    {
        PanelRestart.SetActive(true);
    }
}