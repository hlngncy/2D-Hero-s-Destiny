using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerController : MonoBehaviour,IDamageObserver
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CapsuleCollider2D playerCollider;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask layer;
    
    public bool isDead => _isDead;


    protected IModel _playerModel;
    private Vector2 moveInput;
    private Vector2 _playerVelocity;
    private Coroutine crouchCoroutine;
    private InputAction.CallbackContext moveContext;
    private bool canAttack = true;
    private float attackStartTime = 0;
    private bool isNormalAttack;
    private bool isCrouch = false;
    private bool _isDead;
    
    //events
    [SerializeField] private PlayerEvents _playerEvents;
    
    //views
    [SerializeField] private PlayerUIView _UIView;
    [SerializeField] private AnimationView _animationView;
    
    //models
    [SerializeField] private PlayerModel _playerModelObj;

    private List<IEventListener> _eventListeners = new List<IEventListener>();

    private void Awake()
    {
        _eventListeners.Add(_UIView);
        _eventListeners.Add(_animationView);
        _playerEvents.EventListeners = _eventListeners;
        _playerEvents.AddListeners(_playerModelObj);
        _playerEvents.AddListeners();
        _playerModel = _playerModelObj;
    }

    private void Start()
    {
        _playerVelocity.x = _playerModel.MovementSpeed;
        _playerVelocity.y = _playerModel.JumpPower;
        _UIView.maxHealth = _playerModel.CurrentHealth.Value;
        _playerModel.CurrentHealth.OnValueChanged.AddListener(CheckHealth);
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
        rb.velocity = new Vector2(moveInput.x * _playerVelocity.x,rb.velocity.y);
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
            rb.velocity += rb.velocity = new Vector2(0,moveInput.y * _playerVelocity.y);
            _playerEvents.OnJump();
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.started)
           crouchCoroutine = StartCoroutine(Crouch(context));
    }

    IEnumerator Crouch(InputAction.CallbackContext context)
    {
        isCrouch = true;
        float sizey = playerCollider.size.y;
        
        _playerEvents.OnCrouch(true);
        
        this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 1);
        playerCollider.offset = new Vector2(playerCollider.offset.x, -1.7f);
        _playerVelocity.x *= .5f;
        playerCollider.size = new Vector2(playerCollider.size.x,sizey/2);
        
        while (context.phase != InputActionPhase.Waiting)
        {
            yield return null;
        }
        
        _playerEvents.OnCrouch(false);
        isCrouch = false;
        _playerVelocity.x *= 2f;
        playerCollider.size = new Vector2(playerCollider.size.x,sizey);
        playerCollider.offset = new Vector2(playerCollider.offset.x, -0.55f);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started && (!isNormalAttack || canAttack) && !isCrouch)
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
        if (context.started && canAttack && !isCrouch)
        {
            attackStartTime = Time.time;
            isNormalAttack = false;
            HeavyAttack();
        }
    }

    public void Hurt(int damage)
    {
        _playerEvents.OnHurt(damage , _playerModel.CurrentHealth.Value );
    }
    
    private void CheckHealth(int previousHealth, int currentHealth)
    {
        if(currentHealth <= 0) Die();
    }
    
    public void Die()
    {
        _isDead = true;
        _playerInput.DeactivateInput();
        this.gameObject.layer = LayerMask.NameToLayer("Dead");
        _playerEvents.OnDie();
    }

    public void Tick(float startTime, bool isNormal)
    { 
        if (startTime == 0)
        {
            return;
        }

        canAttack = startTime + (isNormal ? _playerModel.AttackCooldown : _playerModel.HeavyAttackCooldown) < Time.time;
    }

}
