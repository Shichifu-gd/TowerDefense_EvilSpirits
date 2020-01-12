using UnityEngine.EventSystems;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    private TowerStore towerStore { get; set; }
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D raycastHit2D = Physics2D.Raycast(mousePoint, Vector2.zero);
            if (raycastHit2D.collider.tag == "CellForTower")
            {
                raycastHit2D.collider.tag = "CellForTowerFull";
                PlaceTower(raycastHit2D);
            }
        }
        if (spriteRenderer.enabled) FollowMouse();
        if (Input.GetKeyDown(KeyCode.Escape)) DidableDrag();
    }

    public void PlaceTower(RaycastHit2D raycastHit2D)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && towerStore != null)
        {
            GameObject newTower = Instantiate(towerStore.CurrentTower);
            newTower.transform.position = raycastHit2D.transform.position;
            newTower.transform.parent = GameObject.FindGameObjectWithTag("TowerSlot").transform;
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
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = sprite;
    }

    public void DidableDrag()
    {
        spriteRenderer.enabled = false;
        towerStore = null;
    }
}