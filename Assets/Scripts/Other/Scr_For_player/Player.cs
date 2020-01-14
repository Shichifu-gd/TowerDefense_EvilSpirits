using UnityEngine;

public class Player : MonoBehaviour
{
    public UIManager uIManager;

    public int Gold { get; set; }
    public int Life { get; set; }

    private void Start()
    {
        GetGold(Random.Range(50, 70), "plus");
        GetLife(3, "plus");
    }

    public void GetGold(int gold, string option)
    {
        if (option == "plus") Gold += gold;
        if (option == "minus") Gold -= gold;
        uIManager.AssignValuesGoldText(Gold.ToString());
    }

    public void GetLife(int life, string option)
    {
        if (option == "plus") Life += life;
        if (option == "minus") Life -= life;
        uIManager.AssignValuesLifeText(Life.ToString());
    }
}