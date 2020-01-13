using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private EnemySpawnManager enemySpawnManager;
    private Enemy Target;
    private SpriteRenderer ThisSpriteRenderer;
    private ScriptableObjectsProjectile ScrObjProjectile;
    [SerializeField]
    private GameObject PreProjectile;
    public GameObject AdditionalInterface;

    private float BasicDelayAttack;
    private float CurrentDelayAttack;
    private float AttackRange;

    public void Awake()
    {
        enemySpawnManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemySpawnManager>();
        ThisSpriteRenderer = GetComponentInParent<SpriteRenderer>();
    }

    public void AssignTowerValues(ScriptableObjectsTower scrObjTower)
    {
        ThisSpriteRenderer.sprite = scrObjTower.SpriteTower;
        AttackRange = scrObjTower.AttackRangeTower;
        BasicDelayAttack = scrObjTower.BasicDelayAttackTower;
        CurrentDelayAttack = BasicDelayAttack;
        ScrObjProjectile = scrObjTower.ScrObjProjectile;
    }

    private void Update()
    {
        if (CanAttack()) GetTarget();
        if (CurrentDelayAttack > 0) CurrentDelayAttack -= Time.deltaTime;
    }

    private bool CanAttack()
    {
        if (CurrentDelayAttack <= 0) return true;
        return false;
    }

    private void GetTarget()
    {
        Transform target = null;
        float enemyDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemySpawnManager.EnemyList)
        {
            float currentDistance = Vector2.Distance(transform.position, enemy.transform.position);
            if (currentDistance < enemyDistance && currentDistance <= AttackRange)
            {
                target = enemy.transform;
                enemyDistance = currentDistance;
            }
        }
        if (target != null) AttackTower(target);
    }

    /* (delete !!)
    private List<GameObject> GetEnemiesRange()
    {
        List<GameObject> enemyRange = new List<GameObject>();
        foreach (GameObject enemy in enemySpawnManager.EnemyList)
        {
            if (Vector2.Distance(transform.localPosition, enemy.transform.localPosition) <= AttackRange) enemyRange.Add(enemy);
        }
        return enemyRange;
    }
    */

    private void AttackTower(Transform thisEnemy)
    {
        CurrentDelayAttack = BasicDelayAttack;
        GameObject newProjectile = Instantiate(PreProjectile, gameObject.transform.position, Quaternion.identity);
        newProjectile.transform.parent = GameObject.FindGameObjectWithTag("ProjectileSlot").transform;
        newProjectile.GetComponent<Projectile>().AssignValues(thisEnemy, ScrObjProjectile);
    }

    private void OnMouseEnter()
    {
        //  AdditionalInterface.SetActive(true); 
    }

    private void OnMouseExit()
    {
        //  AdditionalInterface.SetActive(false);
    }
}