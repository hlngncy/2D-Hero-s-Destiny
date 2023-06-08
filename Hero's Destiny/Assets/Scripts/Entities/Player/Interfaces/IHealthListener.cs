using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthListener
{
    public void OnHealthChange(int currentHealth, int difference);
}
