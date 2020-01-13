using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player player;
    private EnemySpawnManager enemySpawnManager;
    private Collider2D EnemyCollider2D;
    [SerializeField] private Image ImageCurrentHealth;
    [SerializeField] private SpriteRenderer ThisSpriteRenderer;

    private int AttackEnemy = 1;
    private int DirectionPointIndex = 0;
    private int RewardGold;

    private float SpeedMove;
    private float BaseSpeedMove;
    private float DebuffTime;
    private float PowerDdebuff;
    private float MaxHealthEnemy;
    private float HealthEnemy;

    public bool IsDead { get; set; } = false;

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
        HealthEnemy = scrObjEnemy.HealthEnemy;
        MaxHealthEnemy = HealthEnemy;
        SpeedMove = scrObjEnemy.SpeedMoveEnemy;
        BaseSpeedMove = SpeedMove;
        RewardGold = 15; // add
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
            ProjectileType projectileType = projectile.GetAmmunitionsTypeEnum;
            if (projectile)
            {
                if (projectileType == ProjectileType.FireFist)
                {
                    PowerDdebuff = projectile.EffectImpactProjectile;
                    DebuffTime = projectile.EffectTime;
                    TakingDebuff();
                }
                TakingDamage(projectile.GetAttackProjectile);
                Destroy(collision.gameObject);
            }
        }
    }

    private void EnemyHasReachedFinish()
    {
        player.GetLife(1, "minus");
        enemySpawnManager.UnRegisterEnemy(gameObject);
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
        if ((HealthEnemy - damage) > 0) HealthEnemy -= damage;
        else EnemyIsDead();
        ImageCurrentHealth.fillAmount = HealthEnemy / MaxHealthEnemy;
    }

    public void TakingDebuff()
    {
        SpeedMove = BaseSpeedMove;
        StopCoroutine("slow");
        StartCoroutine(DebuffSpeedReduction());
    }

    IEnumerator DebuffSpeedReduction()
    {
        if (SpeedMove > 0.5f) SpeedMove -= PowerDdebuff;
        else SpeedMove = 0.5f;
        yield return new WaitForSeconds(DebuffTime);
        SpeedMove = BaseSpeedMove;
    }

    private void EnemyIsDead()
    {
        player.GetGold(RewardGold, "plus");
        IsDead = true;
        EnemyCollider2D.enabled = false;
        enemySpawnManager.UnRegisterEnemy(gameObject);
    }
}