
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemy", fileName = "newEnemy", order = 0)]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public short attackRange;
    public ushort attackPower;
    public ushort heavyAttackPower;
    public float heavyAttackCooldown;
    public float attackCooldown;
    public short maxHealth;
    public short movementSpeed;
    public short jumpPower;
}
