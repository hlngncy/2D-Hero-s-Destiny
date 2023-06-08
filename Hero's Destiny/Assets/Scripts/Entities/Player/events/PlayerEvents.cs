using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEvents : MonoBehaviour
{
    private UnityEvent _run = new UnityEvent();
    private UnityEvent _idle = new UnityEvent();
    private UnityEvent _jump = new UnityEvent();
    private UnityEvent<bool> _crouch = new UnityEvent<bool>();
    private UnityEvent _attack = new UnityEvent();
    private UnityEvent _heavyAttack = new UnityEvent();
    private UnityEvent _die = new UnityEvent();
    private UnityEvent<int, int> _hurt = new UnityEvent<int, int>();
    //current health, difference(+,-)



    public void AddListeners(IEventListener listener, PlayerEventEnum playerEvent)
    {
        switch (playerEvent)
        {
            case PlayerEventEnum.Run:
                _run.AddListener(listener.OnRun);
                break;
            case PlayerEventEnum.Idle:
                _idle.AddListener(listener.OnIdle);
                break;
            case PlayerEventEnum.Jump:
                _jump.AddListener(listener.OnJump);
                break;
            case PlayerEventEnum.Crouch:
                _crouch.AddListener(listener.OnCrouch);
                break;
            case PlayerEventEnum.Attack:
                _attack.AddListener(listener.OnAttack);
                break;
            case PlayerEventEnum.HeavyAttack:
                _heavyAttack.AddListener(listener.OnHeavyAttack);
                break;
            case PlayerEventEnum.Die:
                _die.AddListener(listener.OnDie);
                break;
            case PlayerEventEnum.Hurt:
                _hurt.AddListener(listener.OnHurt);
                break;
            default:
                throw new InvalidDataException("Invalid event request.");
        }
    }
    
    public void AddListeners(IModelEventListener listener, PlayerEventEnum playerEvent)
    {
        switch (playerEvent)
        {
            case PlayerEventEnum.Die:
                _die.AddListener(listener.OnDie);
                break;
            case PlayerEventEnum.Hurt:
                _hurt.AddListener(listener.OnHurt);
                break;
            default:
                throw new InvalidDataException("Invalid event request.");
        }
    }

    public void OnRun()
    {
        _run.Invoke();
    }
    public void OnIdle()
    {
        _idle.Invoke();
    }

    public void OnJump()
    {
        _jump.Invoke();
    }
    
    
    public void OnCrouch(bool isCrouch)
    {
        _crouch.Invoke(isCrouch);
    }
    
    
    public void OnAttack()
    {
        _attack.Invoke();
    }
    
    
    public void OnHeavyAttack()
    {
        _heavyAttack.Invoke();
    }
    
    public void OnDie()
    {
        _die.Invoke();
    }
    
    public void OnHurt(int damage, int currentHealth)
    {
        _hurt.Invoke(damage, currentHealth);
    }
    
}


