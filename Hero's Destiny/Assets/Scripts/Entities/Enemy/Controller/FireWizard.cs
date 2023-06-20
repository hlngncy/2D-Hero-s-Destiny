using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FireWizard : EnemyController
{
    private bool _canHeavyAttack;
    private float _heavyAttackCooldown;
    private float _heavyAttackStartTime;
    private int _heavyAttackDamage;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private FireballPool _fireballPool;

    protected override void Awake()
    {
        base.Awake();
        _heavyAttackStartTime = Time.time;
        _heavyAttackCooldown = _enemyStats.heavyAttackCooldown;
        _heavyAttackDamage = _enemyStats.heavyAttackPower;
    }

    protected override void Update()
    {
        base.Update();
        Tick(_heavyAttackStartTime);
    }

    protected override void CheckCanDamage()
    {
        base.CheckCanDamage();
        if(_canHeavyAttack) DoDamage();
    }

    protected override void ChangeState(bool previous, bool current)
    {
        base.ChangeState(previous, current);
        _isTherePlayer.OnValueChanged.RemoveListener(this.ChangeState);
    }
    protected override void DoDamage()
    {
        if (_canHeavyAttack)
        {
            Debug.Log("HeavyAttack");
            _attackType = "heavyattack";
            SpawnFireballs();
            _heavyAttackStartTime = Time.time;
        }
        else
        {
            Debug.Log("Attack");
            _attackType = "attack";
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _enemyStats.attackRange, layer);
            if(hitEnemies.Length == 0) return;
            DamageActionManager.Instance.DoDamage(hitEnemies, _enemyStats.attackPower);
        }
        base.DoDamage();
    }
    
    private void Tick(float startTime)
    {
        _canHeavyAttack = startTime + _heavyAttackCooldown < Time.time;
    }
    
    private void SpawnFireballs()
    {
        for (int i = 0; i < 10; i++)
        {
            float angle = i * Mathf.PI * 2 / 10;
            float x = Mathf.Cos(angle) * 1;
            Vector3 pos = transform.position + new Vector3(x, 0, -2);
            float angleDegrees = -angle * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, 0,angleDegrees);
            _fireballPool.GetFireball(pos, rot, _heavyAttackDamage);
        }

    }
    
}
