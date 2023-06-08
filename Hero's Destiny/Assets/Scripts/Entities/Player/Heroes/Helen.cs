
using UnityEngine;


public class Helen : PlayerController
{
    public override void Run()
    {
        base.Run();
    }

    public override void NormalAttack()
    {
        base.NormalAttack();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, _playerModel.AttackRange,layer);
        DamageActionController.Instance.DoDamage(hitEnemies, _playerModel.AttackPower);
    }
    
    public override void HeavyAttack()
    {
        base.HeavyAttack();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, _playerModel.AttackRange,layer);
        DamageActionController.Instance.DoDamage(hitEnemies, _playerModel.HeavyAttackPower);
    }
    public void HeavyAttackSecondHit()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, _playerModel.AttackRange,layer);
        DamageActionController.Instance.DoDamage(hitEnemies, _playerModel.HeavyAttackPower);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, _playerModel.AttackRange);
    }
}
