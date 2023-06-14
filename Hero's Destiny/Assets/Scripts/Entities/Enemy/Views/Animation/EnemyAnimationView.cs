using DG.Tweening;
using UnityEngine;

public class EnemyAnimationView : MonoBehaviour,IEnemyAnimView
{
    [SerializeField] private Animator _animator;


    public void OnHurt(int damage)
    {
        _animator.SetTrigger("hurt");
    }


    public void OnAttack()
    {
        //_animator.SetTrigger("attack");
    }

    public void OnDead()
    {
        this.transform.DOScale(Vector3.zero, .5f);
    }

    public void OnRun(bool isMoving)
    {
        //_animator.SetInteger("AnimState", isMoving? 1 : 0);
    }
}
