using System.Collections;
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

    public int CurrentLevelTower { get; set; }
    public int MaxLevelTower { get; set; }
    public int PriceForUpgrade { get; set; }

    // |--> for test
    public int DamageBonusPerLevel { get; set; }
    public float AttackRangeBonusPerLevel { get; set; }
    public float BasicDelayAttackBonusPerLevel { get; set; }
    // --<-|

    private float BasicDelayAttack;
    private float CurrentDelayAttack;
    private float AttackRange;

    private bool SwitchButton;

    public void Awake()
    {
        enemySpawnManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemySpawnManager>();
        ThisSpriteRenderer = GetComponentInParent<SpriteRenderer>();
    }

    public void AssignTowerValues(ScriptableObjectsTower scrObjTower)
    {
        CurrentLevelTower = 1;
        MaxLevelTower = scrObjTower.MaxLevel;
        PriceForUpgrade = scrObjTower.PriceForUpgradeTower;
        ThisSpriteRenderer.sprite = scrObjTower.SpriteTower;
        AttackRange = scrObjTower.AttackRangeTower;
        BasicDelayAttack = scrObjTower.BasicDelayAttackTower;
        CurrentDelayAttack = BasicDelayAttack;
        ScrObjProjectile = scrObjTower.ScrObjProjectile;
    }

    public void UpLevelTower()
    {
        DamageBonusPerLevel = ScrObjProjectile.AttackDamageProjectile / 2;
        AttackRangeBonusPerLevel = AttackRange / 3;
        BasicDelayAttackBonusPerLevel = BasicDelayAttack / 3;
        AttackRange += AttackRangeBonusPerLevel;
        BasicDelayAttack += BasicDelayAttackBonusPerLevel;
    }

    private void Update()
    {
        if (CanAttack()) GetTarget();
        if (CurrentDelayAttack > 0) CurrentDelayAttack -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0)) AdditionalInterface.SetActive(true);
        if (SwitchButton == false) AdditionalInterface.SetActive(false);
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

    private void AttackTower(Transform thisEnemy)
    {
        CurrentDelayAttack = BasicDelayAttack;
        GameObject newProjectile = Instantiate(PreProjectile, gameObject.transform.position, Quaternion.identity);
        newProjectile.transform.parent = GameObject.FindGameObjectWithTag("ProjectileSlot").transform;
        newProjectile.GetComponent<Projectile>().AssignValues(thisEnemy, ScrObjProjectile, DamageBonusPerLevel);
    }

    private void OnMouseEnter()
    {
        StartCoroutine(Expectation());
    }

    private void OnMouseExit()
    {
        StartCoroutine(Expectation());
    }

    private IEnumerator Expectation()
    {
        yield return new WaitForSeconds(.1f);
        SwitchButton = !SwitchButton;
    }
}