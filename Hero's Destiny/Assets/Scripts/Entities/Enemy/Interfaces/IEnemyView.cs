using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyUIView
{
    public void OnHurt(HealthInfo healthInfo);
    public void OnDead();
    public void OnEnemyDetect();
    public void OnIdle();
}

public interface IEnemyAnimView
{
    public void OnHurt(HealthInfo healthInfo);
    public void OnAttack(string attack);
    public void OnDead();
    public void OnRun(bool isMoving);
}


