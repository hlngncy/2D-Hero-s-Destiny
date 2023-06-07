using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerController : MonoBehaviour,IDamageObserver
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] protected PlayerSO playerStats;
    [SerializeField] private BoxCollider2D playerCollider;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask layer;
    private Vector2 moveInput;
    private Vector2 Velocity;
    private Coroutine crouchCoroutine;
    private InputAction.CallbackContext moveContext;
    private bool canAttack = true;
    private float attackStartTime = 0;
    private float attackCooldown;
    private float heavyAttackCooldown;
    private bool isNormalAttack;


    //events
    [SerializeField] private PlayerEvents _playerEvents;
    

    private void Start()
    {
        Velocity.x = playerStats.movementSpeed;
        Velocity.y = playerStats.jumpPower;
        attackCooldown = playerStats.attackCooldown;
        heavyAttackCooldown = playerStats.heavyAttackCooldown;
    }

    private void FixedUpdate()
    {
        Run();
        FlipSprite();
        Tick(attackStartTime, isNormalAttack);
    }
    
    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
            _playerEvents.OnRun();
        }
        else
        {
            _playerEvents.OnIdle();
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
            _playerEvents.OnJump();
        }
        else
        {
            
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.started)
           crouchCoroutine = StartCoroutine(Crouch(context));
    }

    IEnumerator Crouch(InputAction.CallbackContext context)
    {
        float sizey = playerCollider.size.y;
        
        _playerEvents.OnCrouch(true);
        
        this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 1);
        playerCollider.offset = new Vector2(playerCollider.offset.x, -1.7f);
        Velocity.x *= .5f;
        playerCollider.size = new Vector2(playerCollider.size.x,sizey/2);
        
        while (context.phase != InputActionPhase.Waiting)
        {
            yield return null;
        }
        
        _playerEvents.OnCrouch(false);
        
        Velocity.x *= 2f;
        playerCollider.size = new Vector2(playerCollider.size.x,sizey);
        playerCollider.offset = new Vector2(playerCollider.offset.x, -0.55f);
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
        _playerEvents.OnAttack();
    }

    public virtual void HeavyAttack()
    {
        _playerEvents.OnHeavyAttack();
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
        _playerEvents.OnHurt(damage);
    }
    public void Die()
    {
        _playerEvents.OnDie();
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