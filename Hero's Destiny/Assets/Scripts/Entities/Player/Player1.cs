using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Player1 : MonoBehaviour
{
    [SerializeField] public Animator _animator;
    [SerializeField] private Rigidbody2D rb;
    private Vector2 moveInput;
    [SerializeField] private Vector2 Velocity;
    [SerializeField] private BoxCollider2D playerCollider;
    public InputBinding Move;
    [SerializeField] private float crouchDuration = 5;
    private Coroutine crouchCoroutine;
    private bool isCrouch;


    private void Update()
    {
        Run();
        FlipSprite();
        CheckAnimStates();
    }

    private void CheckAnimStates()
    {
        _animator.SetFloat("AirSpeedY", rb.velocity.y);
        _animator.SetBool("isGrounded", playerCollider.IsTouchingLayers(LayerMask.GetMask("Platform")));
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
            _animator.SetInteger("AnimState",1);
        }
        else
        {
            _animator.SetInteger("AnimState",0);
        }
    }

    public virtual void Run()
    {
        rb.velocity = new Vector2(moveInput.x * Velocity.x,rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    

    public void OnJump(InputAction.CallbackContext context)
    {
        moveInput.y = 1;
        if (!playerCollider.IsTouchingLayers(LayerMask.GetMask("Platform")))
        {
            return;
        }

        if (context.started)
        {
            rb.velocity += rb.velocity = new Vector2(0,moveInput.y * Velocity.y);
            _animator.SetTrigger("Jump");
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        isCrouch = true;
        if (context.started)
           crouchCoroutine = StartCoroutine(Crouch(context));
    }

    IEnumerator Crouch(InputAction.CallbackContext context)
    {
        float sizex = playerCollider.size.x;
        
        _animator.SetBool("Crouch",true);
        Velocity.x *= .5f;
        playerCollider.size = new Vector2(sizex / 2, playerCollider.size.y);
        while (context.phase != InputActionPhase.Waiting)
        {
            yield return null;
        }
        _animator.SetBool("Crouch", false);
        Velocity.x *= 2f;
        playerCollider.size = new Vector2(sizex, playerCollider.size.y);
        isCrouch = false;
        isCrouch = false;
    }
}
