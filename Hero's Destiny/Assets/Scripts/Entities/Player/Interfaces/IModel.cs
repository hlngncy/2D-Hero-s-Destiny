using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModel
{
    public Observable<int> CurrentHealth { get; }
    public int JumpPower { get; }
    public int MovementSpeed { get; }
    public int AttackRange { get; }
    public int AttackPower { get; }
    public int HeavyAttackPower { get; }
    public float HeavyAttackCooldown { get; }
    public float AttackCooldown { get; }

}
