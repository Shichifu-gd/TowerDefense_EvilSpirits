using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player player;
    private EnemySpawnManager enemySpawnManager;
    private Collider2D EnemyCollider2D;
    public Image ImageCurrentHealth;
    [SerializeField] private SpriteRenderer ThisSpriteRenderer;

    private int MaxHealthEnemy;
    public int HealthEnemy;
    private int DirectionPointIndex = 0;
    public int RewardGold;
    public int RewardScore;

    private float SpeedMove;
    private float BaseSpeedMove;
    private float DebuffTime;
    private float PowerDebuff;

    public bool IsDead { get; set; } = false;

    [HideInInspector]
    public List<GameObject> DirectionPoints;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        DirectionPoints = GameObject.FindGameObjectWithTag("PathGeneration").GetComponent<ListOfDirectionPoints>().ListDirectionPoints;
        enemySpawnManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemySpawnManager>();
        EnemyCollider2D = GetComponent<Collider2D>();
    }

    public void AssignValuesForEnemy(ScriptableObjectsEnemy scrObjEnemy)
    {
        enemySpawnManager.RegisterEnemy(gameObject);
        ThisSpriteRenderer.sprite = scrObjEnemy.SpriteEnemy;
        ThisSpriteRenderer.color = scrObjEnemy.SpriteColor;
        HealthEnemy = scrObjEnemy.HealthEnemy;
        MaxHealthEnemy = HealthEnemy;
        SpeedMove = scrObjEnemy.SpeedMoveEnemy;
        BaseSpeedMove = SpeedMove;
        RewardGold = Random.Range(scrObjEnemy.MinReward, scrObjEnemy.MaxReward);
        RewardScore = Random.Range(scrObjEnemy.MinScore, scrObjEnemy.MaxScore);
    }

    private void FixedUpdate()
    {
        if (IsDead == false) GoToPoint();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish") EnemyHasReachedFinish();
        if (collision.tag == "Ammunition")
        {
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();
            ProjectileType projectileType = projectile.ProjectileTypeEnum;
            if (projectile)
            {
                if (projectileType == ProjectileType.FireFist)
                {
                    PowerDebuff = projectile.EffectImpactProjectile;
                    DebuffTime = projectile.EffectTime;
                    TakingDebuff();
                }
                TakingDamage(projectile.AttackDamage);
                Destroy(collision.gameObject);
            }
        }
    }

    private void EnemyHasReachedFinish()
    {
        player.GetLife(1, "minus");
        enemySpawnManager.UnRegisterEnemy(gameObject, 0);
    }

    private void GoToPoint()
    {
        Vector2 vector = DirectionPoints[DirectionPointIndex].transform.position - transform.position;
        transform.Translate(vector.normalized * Time.deltaTime * SpeedMove);
        if (Vector2.Distance(transform.position, DirectionPoints[DirectionPointIndex].transform.position) < .02f)
        {
            if (DirectionPointIndex < DirectionPoints.Count - 1) DirectionPointIndex++;
            else Destroy(gameObject);
        }
    }

    public void TakingDamage(int damage)
    {
        if ((HealthEnemy - damage) > 0)
        {
            HealthEnemy -= damage;
            float num = HealthEnemy;
            ImageCurrentHealth.fillAmount = num / MaxHealthEnemy;
        }
        else EnemyIsDead();
    }

    public void TakingDebuff()
    {
        SpeedMove = BaseSpeedMove;
        StopCoroutine("slow");
        StartCoroutine(DebuffSpeedReduction());
    }

    IEnumerator DebuffSpeedReduction()
    {
        if (SpeedMove > 0.5f) SpeedMove -= PowerDebuff;
        else SpeedMove = 0.5f;
        yield return new WaitForSeconds(DebuffTime);
        SpeedMove = BaseSpeedMove;
    }

    private void EnemyIsDead()
    {
        player.GetGold(RewardGold, "plus");
        IsDead = true;
        EnemyCollider2D.enabled = false;
        enemySpawnManager.UnRegisterEnemy(gameObject, RewardScore);
    }
}