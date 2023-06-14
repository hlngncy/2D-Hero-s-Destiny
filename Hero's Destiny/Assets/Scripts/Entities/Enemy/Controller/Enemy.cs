using System;
using System.Collections;
using Pathfinding;
using UnityEngine;
using UnityEngine.Events;

public class  Enemy : MonoBehaviour,IController
{

    //views
    [SerializeField] private EnemyAnimationView _enemyAnimationView;
    [SerializeField] private EnemyUIView _enemyUIView;
    
    //model
    [SerializeField] private EnemySO _enemyStats;

    //events
    private UnityEvent _attack = new UnityEvent();
    private UnityEvent _heavyAttack = new UnityEvent();
    private UnityEvent<int> _hurt = new UnityEvent<int>();
    private UnityEvent _dead = new UnityEvent();
    private UnityEvent<bool> _run = new UnityEvent<bool>();
    private UnityEvent _idle = new UnityEvent();
    private UnityEvent _enemyDetect = new UnityEvent();

    //fields
    [SerializeField] private LayerMask layer;
    public bool isDead => _isDead;
    private bool _isDead;
    private IEnumerator _attackRoutine;
    private bool _canAttack = true;
    private float _attackCooldown;
    private float _attackStartTime;
    private int _maxHealth;

    //A* Pathfinder
    [SerializeField] private Collider2D _attackArea;
    [SerializeField] private AIPath _path;
    [SerializeField] private AIDestinationSetter _aiDestinationSetter;
    [SerializeField] private Patrol _patrol;

    private void Awake()
    {
        _path.maxSpeed = _enemyStats.movementSpeed;
        _path.endReachedDistance = _enemyStats.attackRange;
        _maxHealth = _enemyStats.maxHealth;
        _attackCooldown = _enemyStats.attackCooldown;
        
        _hurt.AddListener(_enemyUIView.OnHurt); 
        _dead.AddListener(_enemyUIView.OnDead);
        _enemyDetect.AddListener(_enemyUIView.OnEnemyDetect);
        _idle.AddListener(_enemyUIView.OnIdle);
        
        _hurt.AddListener(_enemyAnimationView.OnHurt);
        _attack.AddListener(_enemyAnimationView.OnAttack);
        _dead.AddListener(_enemyAnimationView.OnDead);
        _run.AddListener(_enemyAnimationView.OnRun);
    }

    private void FixedUpdate()
    {
        FlipSprite();
    }

    private void Update()
    {
        CheckPlayer();
        Tick(_attackStartTime);
    }

    private void CheckPlayer()
    {
        bool isTherePlayer = _attackArea.IsTouchingLayers(layer);
        _aiDestinationSetter.enabled = isTherePlayer;
        _patrol.enabled = !isTherePlayer;
        if(isTherePlayer) _enemyDetect.Invoke();
        else _idle.Invoke();
        if (_path.whenCloseToDestination == CloseToDestinationMode.Stop && isTherePlayer && _canAttack)
        {
            DoDamage();
        }
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(_path.desiredVelocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            _run.Invoke(playerHasHorizontalSpeed);
            transform.localScale = new Vector2(Mathf.Sign(_path.desiredVelocity.x), 1f);
        }
    }

    public void Hurt(int damage)
    {
        _maxHealth -= damage;
        if(_maxHealth <= 0) Die();
        _hurt.Invoke(damage);
    }

    
    private void DoDamage()
    {
        _attackStartTime = Time.time;
        _attack.Invoke();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, _enemyStats.attackRange, layer);
        if(hitEnemies.Length == 0) return;
        DamageActionManager.Instance.DoDamage(hitEnemies, _enemyStats.attackPower);
    }

    public void Die()
    {
        _path.enabled = false;
        this.gameObject.layer = LayerMask.NameToLayer("Dead");
        _isDead = true;
        _dead.Invoke();
        this.enabled = false;
    }
    public void Tick(float startTime)
    { 
        if (startTime == 0)
        {
            return;
        }
        _canAttack = startTime + _attackCooldown < Time.time;
    }
}

