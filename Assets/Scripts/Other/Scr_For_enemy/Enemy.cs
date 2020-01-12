using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemySpawnManager enemySpawnManager;

    private Collider2D enemyCollider2D;

    public int Health;

    public int HealthEnemy { get; set; } = 20;
    public int AttackEnemy { get; set; }
    private int Speed = 2;
    private int Index = 0;

    public bool IsDead { get; set; } = false;

    public List<GameObject> DirectionPoints;

    private void Awake()
    {
        DirectionPoints = GameObject.FindGameObjectWithTag("PathGeneration").GetComponent<ListOfDirectionPoints>().ListDirectionPoints;
        enemySpawnManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemySpawnManager>();
        enemyCollider2D = GetComponent<Collider2D>();
    }

    private void Start()
    {
        enemySpawnManager.RegisterEnemy(this);
    }

    private void FixedUpdate()
    {
        if (IsDead == false) GoToPoint();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish") enemySpawnManager.UnRegisterEnemy(this);
        if (collision.tag == "Ammunition")
        {
            Ammunitions ammunition = collision.gameObject.GetComponent<Ammunitions>();
            if (ammunition)
            {
                TakingDamage(ammunition.AttackDamage);
                Destroy(collision.gameObject);
            }
        }
    }

    private void GoToPoint()
    {
        Vector2 vector = DirectionPoints[Index].transform.position - transform.position;
        transform.Translate(vector.normalized * Time.deltaTime * Speed);
        if (Vector2.Distance(transform.position, DirectionPoints[Index].transform.position) < 0.01f)
        {
            if (Index < DirectionPoints.Count - 1) Index++;
            else Destroy(gameObject);
        }
    }

    public void TakingDamage(int damage)
    {
        Health = HealthEnemy;
        if ((HealthEnemy - damage) > 0) HealthEnemy -= damage;
        else DieEnemy();
    }

    private void DieEnemy()
    {
        IsDead = true;
        enemyCollider2D.enabled = false;
        enemySpawnManager.UnRegisterEnemy(this);
    }

    public void TakingDebuffs()
    {

    }
}