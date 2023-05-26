using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Helen : Player
{
    public override void Run()
    {
        base.Run();
    }

    public override void NormalAttack()
    {
        base.NormalAttack();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, playerStats.attackRange,layer);
        DamageActionController.Instance.DoDamage(hitEnemies, playerStats.attackPower);
    }
    
    public override void HeavyAttack()
    {
        base.HeavyAttack();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, playerStats.heavyAttackCooldown,layer);
        DamageActionController.Instance.DoDamage(hitEnemies, playerStats.heavyAttackPower);
    }
    public void HeavyAttackSecondHit()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, playerStats.heavyAttackCooldown,layer);
        DamageActionController.Instance.DoDamage(hitEnemies, playerStats.heavyAttackPower);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, playerStats.attackRange);
    }
}
