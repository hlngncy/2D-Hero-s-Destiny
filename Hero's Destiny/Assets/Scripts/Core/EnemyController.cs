using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]public List<Enemy> Enemies;

    public List<Enemy> GetEnemiesOnRange(int attackRange, Transform ownerPos)
    {
        List<Enemy> enemiesOnRange = new List<Enemy>();
        for (int i = 0; i < Enemies.Count; i++)
        {
           if(attackRange >= Vector2.Distance(ownerPos.position, Enemies[i].transform.position)) enemiesOnRange.Add(Enemies[i]);
        }
        return enemiesOnRange;
    }
}
