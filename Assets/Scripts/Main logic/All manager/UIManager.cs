using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text GoldText;
    public Text LifeText;

    public void AssignValuesGoldText(string gold)
    {
        GoldText.text = gold;
    }

    public void AssignValuesLifeText(string life)
    {
        LifeText.text = life;
    }
}