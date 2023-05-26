using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player", fileName = "newPlayer", order = 0)]
public class PlayerSO : ScriptableObject
{
    public string playerName;
    public short attackRange;
    public ushort attackPower;
    public ushort heavyAttackPower;
    public float heavyAttackCooldown;
    public float attackCooldown;
    public short maxHealth;
    public short movementSpeed;
    public short jumpPower;
}
