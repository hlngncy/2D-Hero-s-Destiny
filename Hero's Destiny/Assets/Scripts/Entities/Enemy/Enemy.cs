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
    
    
    
    private void Start()
    {
        StartCoroutine(DoDamageC());
    }

    private void FixedUpdate()
    {
        FlipSprite();
    }

    private void Update()
    {
        CheckPlayer();
    }

    private void CheckPlayer()
    {
        bool isTherePlayer = _attackArea.IsTouchingLayers(layer);
        _aiDestinationSetter.enabled = isTherePlayer;
        _patrol.enabled = !isTherePlayer;

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

    private IEnumerator DoDamageC()
    {
        while (true)
        {
            DoDamage();
            yield return new WaitForSeconds(2);
        }
    }
    private void DoDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 5, layer);
        if(hitEnemies.Length == 0) return;
        DamageActionManager.Instance.DoDamage(hitEnemies, 10);
    }

}

