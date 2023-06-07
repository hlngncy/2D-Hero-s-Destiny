using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationView : MonoBehaviour,IView, IEventListener
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerEvents events;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D playerCollider;
    private void Start()
    {
        events.AddListeners(this, PlayerEventEnum.Run);
        events.AddListeners(this, PlayerEventEnum.Idle);
        events.AddListeners(this, PlayerEventEnum.Attack);
        events.AddListeners(this, PlayerEventEnum.HeavyAttack);
        events.AddListeners(this, PlayerEventEnum.Jump);
        events.AddListeners(this, PlayerEventEnum.Crouch);
        events.AddListeners(this, PlayerEventEnum.Die);
        events.AddListeners(this, PlayerEventEnum.Hurt);
    }

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
        Debug.Log("jump");
        _animator.SetTrigger("Jump");
    }

    public void OnCrouch(bool isCrouch)
    {
        _animator.SetBool("Crouch",isCrouch);
        if (_animator.GetBool("isGrounded")) _animator.ResetTrigger("Jump");
        Debug.Log("crouch");
    }
    

    public void OnHurt(int damage)
    {
        _animator.SetTrigger("Hurt");
    }

    public void OnDie()
    {
        _animator.SetTrigger("Hurt");
    }

    public void OnAttack()
    {
        _animator.SetTrigger("Attack");
    }

    public void OnHeavyAttack()
    {
        _animator.SetTrigger("HeavyAttack");
    }
}
