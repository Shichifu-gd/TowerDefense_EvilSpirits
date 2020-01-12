using UnityEngine;

public enum AmmunitionsType
{
    FireFist, StoneLevelOne, StoneLevelTwo,
    IronLevelOne, IronLevelTwo
};

public class Ammunitions : MonoBehaviour
{
    [SerializeField] private int attackDamage;
    [SerializeField] private AmmunitionsType ammunitionsTypeEnum;

    public int AttackDamage
    {
        get
        {
            return attackDamage;
        }
    }

    public AmmunitionsType AmmunitionsTypeEnum
    {
        get
        {
            return ammunitionsTypeEnum;
        }
    }
}