using UnityEngine;

public class Projectile : MonoBehaviour
{
    private ScriptableObjectsTower ScrObjProjectile;
    public SpriteRenderer SpriteRendererProjectile;
    public ProjectileType ProjectileTypeEnum { get; set; }

    private Transform Target;

    public int AttackDamage;

    public float ProjectileSpeed;
    public float EffectImpactProjectile;
    public float EffectTime;

    public bool StartMove { get; set; }

    private void Update()
    {
        if (StartMove == true) MoveProjectile();
    }

    public void AssignValues(Transform enemy, ScriptableObjectsProjectile scrObjProjectile)
    {
        ProjectileTypeEnum = scrObjProjectile.ProjectileTypeEnum;
        ProjectileSpeed = scrObjProjectile.SpeedMoveProjectile;
        SpriteRendererProjectile.sprite = scrObjProjectile.SpriteProjectile;
        AttackDamage = scrObjProjectile.AttackDamageProjectile;
        EffectImpactProjectile = scrObjProjectile.EffectImpactProjectile;
        EffectTime = scrObjProjectile.EffectTime;
        Target = enemy;
        StartMove = true;
    }

    private void MoveProjectile()
    {
        if (Target != null)
        {
            if (Vector2.Distance(transform.position, Target.position) < .2f) Destroy(gameObject);
            else
            {
                Vector2 direction = Target.transform.position - transform.position;
                transform.Translate(direction.normalized * Time.deltaTime * ProjectileSpeed);
            }
        }
        else Destroy(gameObject);
    }

    public int GetAttackProjectile
    {
        get
        {
            return AttackDamage;
        }
    }

    public float GetEffectProjectile
    {
        get
        {
            return EffectImpactProjectile;
        }
    }

    public float GetEffectTimeProjectile
    {
        get
        {
            return EffectTime;
        }
    }

    public ProjectileType GetAmmunitionsTypeEnum
    {
        get
        {
            return ProjectileTypeEnum;
        }
    }
}