using System;
using DG.Tweening;
using UnityEngine;

public class EnemyAnimationView : MonoBehaviour,IEnemyAnimView
{
    [SerializeField] private Animator _animator;
    

    private void Update()
    {
        
    }

    public void OnHurt(int damage)
    {
        _animator.SetTrigger("hurt");
    }

    public void OnAttack()
    {
        _animator.SetTrigger("attack");
    }

    public void OnDead()
    {
        _animator.SetBool("isDead", true);
        _animator.SetTrigger("die");
    }

    public void OnRun(bool isMoving)
    {
        _animator.SetInteger("AnimState", isMoving? 1 : 0);
    }
}
