using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Tower")]
public class ScriptableObjectsTower : ScriptableObject
{
    public ScriptableObjectsProjectile ScrObjProjectile;
    public Sprite SpriteTower;
    public float BasicDelayAttackTower;
    public float AttackRangeTower;
    public int PriceTower;
}