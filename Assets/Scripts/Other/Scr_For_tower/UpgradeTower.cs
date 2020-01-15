using UnityEngine;

public class UpgradeTower : MonoBehaviour
{
    private Player player;
    public Tower tower;
    public GameObject CurrentCell { get; set; }
    public GameObject ButtunUpgradeTower;
    private SpriteRenderer SpriteRendererTower;

    //-|>- replace with sprites !!
    public Color ColorForLevelTwo;
    public Color ColorForLevelThree;
    //-<-|

    private void Awake()
    {
        SpriteRendererTower = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void TowerUpgrade()
    {
        if (tower.CurrentLevelTower <= tower.MaxLevelTower)
        {
            if (player.Gold >= tower.PriceForUpgrade)
            {
                tower.UpLevelTower();
                tower.CurrentLevelTower++;
                player.GetGold(tower.PriceForUpgrade, "minus");
            }
            if (tower.CurrentLevelTower > tower.MaxLevelTower) ButtunUpgradeTower.SetActive(false);
            if (tower.CurrentLevelTower == 2) SpriteRendererTower.color = ColorForLevelTwo;
            if (tower.CurrentLevelTower == 3) SpriteRendererTower.color = ColorForLevelThree;
        }
        else ButtunUpgradeTower.SetActive(false);
    }

    public void TowerDestroy()
    {
        CurrentCell.tag = "CellForTower";
        Destroy(gameObject);
    }
}