using UnityEngine;

public class DeterminantVectorCurrentPosition : MonoBehaviour
{
    [HideInInspector]
    public GameObject CurrentCell;
    public GameObject North;
    public GameObject South;
    public GameObject East;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cell")) CurrentCell = collision.gameObject;
    }
}