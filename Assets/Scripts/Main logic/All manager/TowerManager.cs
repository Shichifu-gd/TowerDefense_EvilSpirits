using UnityEngine.EventSystems;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public Player player;
    private TowerStore towerStore;
    private SpriteRenderer SpriteSelectedTower;

    private void Start()
    {
        SpriteSelectedTower = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D raycastHit2D = Physics2D.Raycast(mousePoint, Vector2.zero);
            if (towerStore && raycastHit2D.collider.tag == "CellForTower")
            {
                if (player.Gold >= towerStore.TowerPrice)
                {
                    raycastHit2D.collider.tag = "CellForTowerFull";
                    BuildingTower(raycastHit2D);
                }
                else DisableDrag();
            }
        }
        if (SpriteSelectedTower.enabled) FollowMouse();
        if (Input.GetKeyDown(KeyCode.Escape)) DisableDrag();
    }

    public void BuildingTower(RaycastHit2D raycastHit2D)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && towerStore != null)
        {
            GameObject newTower = Instantiate(towerStore.CurrentTower);
            newTower.GetComponentInChildren<Tower>().AssignTowerValues(towerStore.ScrObjTower);
            newTower.transform.position = raycastHit2D.transform.position;
            newTower.GetComponent<UpgradeTower>().CurrentCell = raycastHit2D.collider.gameObject;
            newTower.transform.parent = GameObject.FindGameObjectWithTag("TowerSlot").transform;
            player.GetGold(towerStore.TowerPrice, "minus");
            DisableDrag();
        }
    }

    public void SelectedTower(TowerStore towerSelected)
    {
        towerStore = towerSelected;
        EnableDrag(towerStore.SpriteTower);
    }

    public void FollowMouse()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(transform.position.x, transform.position.y);
    }

    public void EnableDrag(Sprite sprite)
    {
        SpriteSelectedTower.enabled = true;
        SpriteSelectedTower.sprite = sprite;
    }

    public void DisableDrag()
    {
        SpriteSelectedTower.enabled = false;
        towerStore = null;
    }
}