using UnityEngine;

public class GettingCellReference : MonoBehaviour
{
    [HideInInspector]
    public GameObject CellPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cell")) CellPosition = collision.gameObject;
    }
}