using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private AudioSource _source;

    private void Start()
    {
        this.transform.DOMoveY(transform.position.y + 2f, .5f).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            DamageActionManager.Instance.DoDamage(col.collider,-_health);
            _source.Play();
            Destroy(gameObject);
        }
    }
}
