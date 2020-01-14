using UnityEngine;

 public enum ProjectileType { FireFist, Stone, Iron, };

[CreateAssetMenu(menuName = "ScriptableObjects/Projectile")]
public class ScriptableObjectsProjectile : ScriptableObject
{
    public Sprite SpriteProjectile;
    public ProjectileType ProjectileTypeEnum;
    public float SpeedMoveProjectile;
    public float EffectImpactProjectile;
    public float EffectTime;
    public int AttackDamageProjectile;
}