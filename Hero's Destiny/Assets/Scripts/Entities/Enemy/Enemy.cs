using System;
using System.Collections;
using Pathfinding;
using UnityEngine;

public class Enemy : MonoBehaviour,IDamageObserver
{
    [SerializeField] private LayerMask layer;
    [SerializeField] private AIPath _path;
    private int maxHealth = 100;
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider2D _attackArea;
    [SerializeField] private AIDestinationSetter _aiDestinationSetter;
    [SerializeField] private Patrol _patrol;
    public bool isDead => _isDead;
    private bool _isDead;
    private IEnumerator _attackRoutine;
    private bool _canAttack = true;
    private float _attackCooldown = 3;
    private float _attackStartTime;


    private void Start()
    {
        
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
            transform.localScale = new Vector2(Mathf.Sign(_path.desiredVelocity.x), 1f);
        }
    }

    public void Hurt(int damage)
    {
        maxHealth -= damage;
        _animator.SetTrigger("hurt");
    }

    
    private void DoDamage()
    {
        _attackStartTime = Time.time;
        Debug.Log("enemy attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 5, layer);
        if(hitEnemies.Length == 0) return;
        DamageActionManager.Instance.DoDamage(hitEnemies, 10);
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

