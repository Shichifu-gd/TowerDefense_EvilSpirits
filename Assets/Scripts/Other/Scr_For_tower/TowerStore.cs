using UnityEngine;

public class TowerStore : MonoBehaviour
{
    public GameObject currentTower;
    public Sprite spriteTower;

    [SerializeField]
    private int towerPrice;

    public GameObject CurrentTower
    {
        get
        {
            return currentTower;
        }
    }

    public Sprite SpriteTower
    {
        get
        {
            return spriteTower;
        }
    }

    public int TowerPrice
    {
        get
        {
            return towerPrice;
        }
    }
}