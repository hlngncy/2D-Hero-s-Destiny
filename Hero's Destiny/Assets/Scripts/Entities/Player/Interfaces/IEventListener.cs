using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IEventListener
{
    public void OnRun();
    public void OnIdle();
    public void OnJump();
    public void OnCrouch(bool isCrouch);
    public void OnHurt(int damage);
    public void OnDie();
    public void OnAttack();
    public void OnHeavyAttack();
}
