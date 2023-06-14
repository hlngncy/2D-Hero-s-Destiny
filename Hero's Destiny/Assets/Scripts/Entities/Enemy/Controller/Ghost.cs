using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : EnemyController
{
    protected override void DoDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, _enemyStats.attackRange, layer);
        if(hitEnemies.Length == 0) return;
        DamageActionManager.Instance.DoDamage(hitEnemies, _enemyStats.attackPower);
        base.DoDamage();
    }
}
