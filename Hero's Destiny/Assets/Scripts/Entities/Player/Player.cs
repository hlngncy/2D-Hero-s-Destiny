using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Player : MonoBehaviour,IDamageObserver
{
    [SerializeField] protected Animator _animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] protected PlayerSO playerStats;
    [SerializeField] private BoxCollider2D playerCollider;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask layer;
    protected List<Collider2D> hitEnemies;
    private Vector2 moveInput;
    private Vector2 Velocity;
    private Coroutine crouchCoroutine;
    private bool isCrouch;
    private InputAction.CallbackContext moveContext;
    private bool canAttack = true;
    private float attackStartTime = 0;
    private float attackCooldown;
    private float heavyAttackCooldown;
    private bool isNormalAttack;


    private void Start()
    {
        Velocity.x = playerStats.movementSpeed;
        Velocity.y = playerStats.jumpPower;
        attackCooldown = playerStats.attackCooldown;
        heavyAttackCooldown = playerStats.heavyAttackCooldown;
    }

    private void Update()
    {
        Run();
        FlipSprite();
        CheckAnimStates();
        Tick(attackStartTime, isNormalAttack);
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
        else
        {
            
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
        float sizey = playerCollider.size.y;
        
        _animator.SetBool("Crouch",true);
        this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 1);
        playerCollider.offset = new Vector2(playerCollider.offset.x, -1.7f);
        Velocity.x *= .5f;
        playerCollider.size = new Vector2(playerCollider.size.x,sizey/2);
        
        while (context.phase != InputActionPhase.Waiting)
        {
            yield return null;
        }
        
        _animator.SetBool("Crouch", false);
        Velocity.x *= 2f;
        playerCollider.size = new Vector2(playerCollider.size.x,sizey);
        playerCollider.offset = new Vector2(playerCollider.offset.x, -0.55f);
        if(_animator.GetBool("isGrounded")) _animator.ResetTrigger("Jump");
        isCrouch = false;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started && (!isNormalAttack || canAttack))
        {
            attackStartTime = Time.time;
            isNormalAttack = true;
            NormalAttack();
        }
    }


    public virtual void NormalAttack()
    {
        _animator.SetTrigger("Attack");
    }

    public virtual void HeavyAttack()
    {
        Debug.Log("heavyattack");
        _animator.SetTrigger("HeavyAttack");
    }
    public void OnHeavyAttack(InputAction.CallbackContext context)
    {
        if (context.started && canAttack)
        {
            attackStartTime = Time.time;
            isNormalAttack = false;
            HeavyAttack();
        }
    }

    public void Hurt(int damage)
    {
        _animator.SetTrigger("Hurt");
    }

    public void Tick(float startTime, bool isNormal)
    { 
        if (startTime == 0)
        {
            return;
        }

        canAttack = startTime + (isNormal ? attackCooldown : heavyAttackCooldown) < Time.time;
    }
}
