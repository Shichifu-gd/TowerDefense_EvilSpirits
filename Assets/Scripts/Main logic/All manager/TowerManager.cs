using UnityEngine.EventSystems;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public Player player;
    private TowerStore towerStore;
    private SpriteRenderer SpriteSelectedTtower;

    private void Start()
    {
        SpriteSelectedTtower = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D raycastHit2D = Physics2D.Raycast(mousePoint, Vector2.zero);
            if (towerStore && raycastHit2D.collider.tag == "CellForTower")
            {
                if (player.Gold > towerStore.towerPrice)
                {
                    raycastHit2D.collider.tag = "CellForTowerFull";
                    PlaceTower(raycastHit2D);
                }
                else DidableDrag();
            }
        }
        if (SpriteSelectedTtower.enabled) FollowMouse();
        if (Input.GetKeyDown(KeyCode.Escape)) DidableDrag();
    }

    public void PlaceTower(RaycastHit2D raycastHit2D)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && towerStore != null)
        {
            GameObject newTower = Instantiate(towerStore.CurrentTower);
            newTower.GetComponentInChildren<Tower>().AssignTowerValues(towerStore.ScrObjTower);
            newTower.transform.position = raycastHit2D.transform.position;
            newTower.transform.parent = GameObject.FindGameObjectWithTag("TowerSlot").transform;
            player.GetGold(towerStore.towerPrice, "minus");
            DidableDrag();
        }
    }

    public void SelectedTower(TowerStore towerSelected)
    {
        towerStore = towerSelected;
        EnebleDrag(towerStore.SpriteTower);
    }

    public void FollowMouse()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(transform.position.x, transform.position.y);
    }

    public void EnebleDrag(Sprite sprite)
    {
        SpriteSelectedTtower.enabled = true;
        SpriteSelectedTtower.sprite = sprite;
    }

    public void DidableDrag()
    {
        SpriteSelectedTtower.enabled = false;
        towerStore = null;
    }
}