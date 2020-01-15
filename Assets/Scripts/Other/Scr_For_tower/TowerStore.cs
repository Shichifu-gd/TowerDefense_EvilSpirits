using UnityEngine.UI;
using UnityEngine;

public class TowerStore : MonoBehaviour
{
    public ScriptableObjectsTower ScrObjTower;
    [HideInInspector]
    public Sprite SprTower;
    public Image IcoTower;
    public GameObject PreTower;
    public Text DamageText;
    public Text PriceText;

    public int TowerPrice { get; set; }

    private void Start()
    {
        SprTower = ScrObjTower.SpriteTower;
        IcoTower.sprite = ScrObjTower.SpriteTower;
        TowerPrice = ScrObjTower.PriceTower;
        DamageText.text = ScrObjTower.ScrObjProjectile.AttackDamageProjectile.ToString();
        PriceText.text = TowerPrice.ToString();
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