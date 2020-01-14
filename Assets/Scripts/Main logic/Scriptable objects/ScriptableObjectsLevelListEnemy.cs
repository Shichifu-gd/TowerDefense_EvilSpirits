using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Characters/ListEnemy")]
public class ScriptableObjectsLevelListEnemy : ScriptableObject
{
    [SerializeField]
    private string LevelEnemyUnit;
    public AnimationCurve Distribution;
    public ScriptableObjectsEnemy[] EnemyList;
}