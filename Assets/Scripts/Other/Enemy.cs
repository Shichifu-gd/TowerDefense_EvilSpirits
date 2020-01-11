using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int HealthEnemy { get; set; }
    public int AttackEnemy { get; set; }

    private int Speed = 2;
    private int Index = 0;

    public List<GameObject> DirectionPoints;

    private void Awake()
    {
        DirectionPoints = GameObject.FindGameObjectWithTag("PathGeneration").GetComponent<ListOfDirectionPoints>().ListDirectionPoints;
    }

    private void Update()
    {
        GoToPoint();
    }

    private void GoToPoint()
    {
        Vector2 vector = DirectionPoints[Index].transform.position - transform.position;
        transform.Translate(vector.normalized * Time.deltaTime * Speed);
        if (Vector2.Distance(transform.position, DirectionPoints[Index].transform.position) < 0.3f)
        {
            if (Index < DirectionPoints.Count - 1) Index++;
            else Destroy(gameObject);
        }
    }
}