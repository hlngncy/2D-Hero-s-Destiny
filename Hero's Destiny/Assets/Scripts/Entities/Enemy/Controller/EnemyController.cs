
using System.Collections;
using Pathfinding;
using UnityEngine;
using UnityEngine.Events;

public abstract class EnemyController : MonoBehaviour,IController
{

    //views
    [SerializeField] protected EnemyAnimationView _enemyAnimationView;
    [SerializeField] private EnemyUIView _enemyUIView;
    
    //model
    [SerializeField] protected EnemySO _enemyStats;

    //events
    private UnityEvent<string> _attack = new UnityEvent<string>();
    private UnityEvent<int, int, int> _hurt = new UnityEvent<int, int, int>();
    private UnityEvent _dead = new UnityEvent();
    private UnityEvent<bool> _run = new UnityEvent<bool>();
    private UnityEvent _idle = new UnityEvent();
    private UnityEvent _enemyDetect = new UnityEvent();

    //fields
    [SerializeField] protected LayerMask layer;
    public bool isDead => _isDead;
    private bool _isDead;
    private IEnumerator _attackRoutine;
    private bool _canAttack = true;
    private float _attackCooldown;
    private float _attackStartTime;
    private int _maxHealth;
    protected Observable<bool> _isTherePlayer = new Observable<bool>();
    protected string _attackType;
    private bool _isReached;

    //A* Pathfinder
    [SerializeField] private Collider2D _attackArea;
    [SerializeField] private AIPath _path;
    [SerializeField] private AIDestinationSetter _aiDestinationSetter;
    [SerializeField] private Patrol _patrol;

    protected virtual void Awake()
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
        _aiDestinationSetter.target = GameObject.FindWithTag("Player").transform;
        _isTherePlayer.OnValueChanged.AddListener(ChangeState);
        _attackType = "attack";
    }

    private void FixedUpdate()
    {
        FlipSprite();
    }

    protected virtual void Update()
    {
        CheckPlayer();
        Tick(_attackStartTime);
        if(_aiDestinationSetter.enabled) CheckCanDamage();
    }

    private void CheckPlayer()
    {
        bool isTherePlayer = _attackArea.IsTouchingLayers(layer);
        if(_isTherePlayer.Value == isTherePlayer) return;
        else _isTherePlayer.Value = isTherePlayer;
    }

    protected virtual void ChangeState(bool previous, bool current)
    {
        Debug.Log("changestate");
        _aiDestinationSetter.enabled = current;
        _patrol.enabled = !current;
        if(current) _enemyDetect.Invoke();
        else _idle.Invoke();
    }

    protected virtual void CheckCanDamage()
    {
        _isReached = _path.reachedEndOfPath;
        if (_isReached && _isTherePlayer.Value && _canAttack)
        {
            Debug.Log("attack");
            DoDamage();
        }
    }
    
    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(_path.desiredVelocity.x) > Mathf.Epsilon;
        _run.Invoke(playerHasHorizontalSpeed);
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(_path.desiredVelocity.x), 1f);
        }
    }

    public void Hurt(int damage)
    {
        _maxHealth -= damage;
        if(_maxHealth <= 0) Die();
        _hurt.Invoke(damage , _maxHealth, _enemyStats.maxHealth);
    }

    
    protected virtual void DoDamage()
    {
        _attackStartTime = Time.time;
        _attack.Invoke(_attackType);
    }
    public void Die()
    {
        _path.enabled = false;
        _aiDestinationSetter.enabled = false;
        _patrol.enabled = false;
        this.gameObject.layer = LayerMask.NameToLayer("Dead");
        _isDead = true;
        _dead.Invoke();
        this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 4;
        this.enabled = false;
        Invoke(nameof(SetDeactive),3);
        
    }

    public void SetDeactive()
    {
        this.gameObject.SetActive(false);
    }
    private void Tick(float startTime)
    { 
        if (startTime == 0)
        {
            return;
        }
        _canAttack = startTime + _attackCooldown < Time.time;
    }
}

