using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : EnemyController
{
    protected override void DoDamage()
    {
        base.DoDamage();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, _enemyStats.attackRange, layer);
        if(hitEnemies.Length == 0) return;
        DamageActionManager.Instance.DoDamage(hitEnemies, _enemyStats.attackPower);
    }
}
