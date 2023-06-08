using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModelEventListener
{
    public void OnHurt(int damage, int currentHealth);
    public void OnDie();
}
