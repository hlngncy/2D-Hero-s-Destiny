using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro.SpriteAssetUtilities;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _damageCooldown;
    [SerializeField] private Vector3 _patrolPoint;
    private float _attackTime = 0;

    private void Start()
    {
        DoPatrol();
    }

    private void DoPatrol()
    {
        transform.DORotate(
            new Vector3(0, 0, 360), .5f, RotateMode.FastBeyond360).SetLoops(-1,LoopType.Restart).SetEase(Ease.Linear);
        transform.DOMove(_patrolPoint, .8f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear).SetDelay(.1f);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player") && _attackTime + _damageCooldown < Time.time)
        {
            _attackTime = Time.time;
            DamageActionManager.Instance.DoDamage(col.collider, _damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && _attackTime + _damageCooldown < Time.time)
        {
            _attackTime = Time.time;
            DamageActionManager.Instance.DoDamage(col, _damage);
        }
    }
}
