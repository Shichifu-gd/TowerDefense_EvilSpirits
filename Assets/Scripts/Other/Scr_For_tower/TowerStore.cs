using UnityEngine.UI;
using UnityEngine;

public class TowerStore : MonoBehaviour
{
    public ScriptableObjectsTower ScrObjTower;
    public Image IcoTower;
    public GameObject PreTower;
    [HideInInspector]
    public Sprite SprTower;
    public int towerPrice { get; set; }

    public Text DamageText;
    public Text PriceText;

    private void Start()
    {
        SprTower = ScrObjTower.SpriteTower;
        IcoTower.sprite = ScrObjTower.SpriteTower;
        towerPrice = ScrObjTower.PriceTower;
        DamageText.text = ScrObjTower.ScrObjProjectile.AttackDamageProjectile.ToString();
        PriceText.text = towerPrice.ToString();
    }

    public GameObject CurrentTower
    {
        get
        {
            return PreTower;
        }
    }

    public Sprite SpriteTower
    {
        get
        {
            return SprTower;
        }
    }
}