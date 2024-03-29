using UnityEngine;

public class AnimationView : MonoBehaviour,IView, IEventListener
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerEvents events;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D playerCollider;

    private void Update()
    {
        CheckAnimStates();
    }

    private void CheckAnimStates()
    {
        _animator.SetFloat("AirSpeedY", rb.velocity.y);
        _animator.SetBool("isGrounded", playerCollider.IsTouchingLayers(LayerMask.GetMask("Platform")));
        /*if (_animator.GetBool("isGrounded") && _animator.)
        {
            Debug.Log("jump cancelled");
            _animator.ResetTrigger("Jump");
        }*/
    }

    public void OnRun()
    {
        _animator.SetInteger("AnimState",1);
    }

    public void OnIdle()
    {
        _animator.SetInteger("AnimState",0);
    }

    public void OnJump()
    {
        _animator.SetTrigger("Jump");
    }

    public void OnCrouch(bool isCrouch)
    {
        _animator.SetBool("Crouch",isCrouch);
        if (_animator.GetBool("isGrounded")) _animator.ResetTrigger("Jump");
    }
    

    public void OnHurt(HealthInfo healthInfo)
    {
        if (healthInfo.currentHealth - healthInfo.damage <= 0 || healthInfo.damage < 0) return;
        _animator.SetTrigger("Hurt");
    }

    public void OnDie()
    {
        _animator.SetTrigger("Die");
        _animator.SetBool("isDead", true);
    }

    public void OnAttack()
    {
        _animator.SetBool("isAttacking", true);
        _animator.SetTrigger("Attack");
    }

    public void OnHeavyAttack()
    {
        _animator.SetBool("isAttacking", true);
        _animator.SetTrigger("HeavyAttack");
    }

    public void OnAttackEnd()
    {
        _animator.SetBool("isAttacking", false);
    }
}
