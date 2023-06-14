
using UnityEngine;


public class Helen : PlayerController
{

    protected override void NormalAttack()
    {
        base.NormalAttack();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, _playerModel.AttackRange,layer);
        DamageActionManager.Instance.DoDamage(hitEnemies, _playerModel.AttackPower);
    }
    
    protected override void HeavyAttack()
    {
        base.HeavyAttack();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, _playerModel.AttackRange,layer);
        if(hitEnemies.Length == 0) return;
        DamageActionManager.Instance.DoDamage(hitEnemies, _playerModel.HeavyAttackPower);
    }
    public void HeavyAttackSecondHit()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, _playerModel.AttackRange,layer);
        if(hitEnemies.Length == 0) return;
        DamageActionManager.Instance.DoDamage(hitEnemies, _playerModel.HeavyAttackPower);
    }
}
