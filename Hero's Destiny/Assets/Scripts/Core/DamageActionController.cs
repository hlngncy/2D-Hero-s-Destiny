using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageActionController : Singleton<DamageActionController>
{
    public void DoDamage(Collider2D[] entities, int damage)
    {
        if (entities == null) return;
        for (int i = 0; i < entities.Length; i++)
        {
            entities[i].GetComponent<IDamageObserver>().Hurt(damage);
        }
    }
}
