using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyUIView
{
    public void OnHurt(int damage, int currentHealth, int maxHealth);
    public void OnDead();
    public void OnEnemyDetect();
    public void OnIdle();
}

public interface IEnemyAnimView
{
    public void OnHurt(int damage, int currentHealth, int maxHealth);
    public void OnAttack();
    public void OnDead();
    public void OnRun(bool isMoving);
}


