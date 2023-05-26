using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour,IDamageObserver
{
    private void Start()
    {
        
    }

    private int maxHealth = 100;
    [SerializeField] private Animator _animator; 
    public void Hurt(int damage)
    {
        maxHealth -= damage;
        _animator.SetTrigger("hurt");
        Debug.Log(maxHealth);
    }

}
public interface IDamageObserver
{
    public void Hurt(int damage);
}
