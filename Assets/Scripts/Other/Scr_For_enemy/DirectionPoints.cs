using UnityEngine;

public class DirectionPoints : MonoBehaviour
{
    ListOfDirectionPoints listOfDirectionPoints;

    private void Awake()
    {
        listOfDirectionPoints = GameObject.FindGameObjectWithTag("PathGeneration").GetComponent<ListOfDirectionPoints>();
    }

    private void Start()
    {
        listOfDirectionPoints.ListDirectionPoints.Add(gameObject);
    }
}