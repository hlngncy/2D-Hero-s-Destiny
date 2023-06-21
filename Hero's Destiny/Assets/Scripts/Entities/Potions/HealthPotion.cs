using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] private int _health;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            DamageActionManager.Instance.DoDamage(col.collider,-_health);
            Destroy(gameObject);
        }
    }
}
