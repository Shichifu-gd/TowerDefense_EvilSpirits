using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private EnemySpawnManager enemySpawnManager;
    private Enemy target;
    [SerializeField]
    private Ammunitions ammunitionTower;

    [SerializeField]
    private float attackDelay;
    [SerializeField]
    private float attackRadius;
    private float attackCounter;

    private bool isAttacking = false;
    private void Awake()
    {
        enemySpawnManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemySpawnManager>();
    }

    private void Update()
    {
        attackCounter -= Time.deltaTime;
        if (target == null || target.IsDead)
        {
            target = null;
            Enemy nearestEnemy = GetNearestEnemy();
            if (nearestEnemy != null && Vector2.Distance(transform.localPosition, nearestEnemy.transform.localPosition) <= attackRadius) target = nearestEnemy;
        }
        else
        {
            if (attackCounter <= 0)
            {
                isAttacking = true;
                attackCounter = attackDelay;
                if (Vector2.Distance(transform.localPosition, target.transform.localPosition) > attackRadius) target = null;
            }
            else isAttacking = false;
        }
    }

    private void FixedUpdate()
    {
        if (isAttacking == true) AttackTower();
    }

    private void AttackTower()
    {
        isAttacking = false;
        Ammunitions newAmmunitions = Instantiate(ammunitionTower) as Ammunitions;
        newAmmunitions.transform.localPosition = transform.localPosition;
        if (target == null || target.IsDead) Destroy(newAmmunitions);
        else StartCoroutine(MoveAmmunition(newAmmunitions));
    }

    private IEnumerator MoveAmmunition(Ammunitions shell)
    {
        while (GetTargetDistance(target) > 0.20f && shell != null && target != null && !target.IsDead)
        {
            var direction = target.transform.localPosition - transform.localPosition;
            var angleDirection = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            shell.transform.rotation = Quaternion.AngleAxis(angleDirection, Vector3.forward);
            shell.transform.localPosition = Vector2.MoveTowards(shell.transform.localPosition, target.transform.localPosition, 5f * Time.deltaTime);
            yield return null;
        }
        if (shell != null || target == null || target.IsDead) Destroy(shell);
    }

    private float GetTargetDistance(Enemy thisEnemy)
    {
        if (thisEnemy == null)
        {
            thisEnemy = GetNearestEnemy();
            if (thisEnemy == null) return 0f;
        }
        return Mathf.Abs(Vector2.Distance(transform.localPosition, thisEnemy.transform.localPosition));
    }

    private Enemy GetNearestEnemy()
    {
        Enemy nearestEnemy = null;
        float smallDistance = float.PositiveInfinity;
        foreach (Enemy enemy in GetEnemiesRange())
        {
            if (Vector2.Distance(transform.localPosition, enemy.transform.localPosition) < smallDistance)
            {
                smallDistance = Vector2.Distance(transform.localPosition, enemy.transform.localPosition);
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }

    private List<Enemy> GetEnemiesRange()
    {
        List<Enemy> enemyRange = new List<Enemy>();
        foreach (Enemy enemy in enemySpawnManager.EnemyList)
        {
            if (Vector2.Distance(transform.localPosition, enemy.transform.localPosition) <= attackRadius) enemyRange.Add(enemy);
        }
        return enemyRange;
    }
}