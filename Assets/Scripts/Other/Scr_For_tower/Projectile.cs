using UnityEngine;

public class Projectile : MonoBehaviour
{
    private ScriptableObjectsTower ScrObjProjectile;
    public SpriteRenderer SpriteRendererProjectile;
    public ProjectileType ProjectileTypeEnum { get; set; }

    private Transform Target;

    public int AttackDamage { get; set; }

    public float ProjectileSpeed { get; set; }
    public float EffectImpactProjectile { get; set; }
    public float EffectTime { get; set; }

    public bool StartMove { get; set; }

    private void Update()
    {
        if (StartMove == true) MoveProjectile();
    }

    public void AssignValues(Transform enemy, ScriptableObjectsProjectile scrObjProjectile, int damageBonus)
    {
        ProjectileTypeEnum = scrObjProjectile.ProjectileTypeEnum;
        ProjectileSpeed = scrObjProjectile.SpeedMoveProjectile;
        SpriteRendererProjectile.sprite = scrObjProjectile.SpriteProjectile;
        AttackDamage = scrObjProjectile.AttackDamageProjectile + damageBonus;
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
}