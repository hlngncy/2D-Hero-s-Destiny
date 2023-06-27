using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEvents : MonoBehaviour
{
    private HealthInfo _healthInfo;
    private UnityEvent _run = new UnityEvent();
    private UnityEvent _idle = new UnityEvent();
    private UnityEvent _jump = new UnityEvent();
    private UnityEvent<bool> _crouch = new UnityEvent<bool>();
    private UnityEvent _attack = new UnityEvent();
    private UnityEvent _heavyAttack = new UnityEvent();
    private UnityEvent _die = new UnityEvent();
    private UnityEvent<HealthInfo> _hurt = new UnityEvent<HealthInfo>();
    private UnityEvent _heal = new UnityEvent();

    public List<IEventListener> EventListeners
    {
        set => _eventListeners = value;
    }
    private List<IEventListener> _eventListeners = new List<IEventListener>();
    private void Awake()
    {
        _healthInfo = new HealthInfo();
        DontDestroyOnLoad(this.gameObject);
    }

    public void AddListeners()
    {
        for (int i = 0; i < _eventListeners.Count ; i++)
        {
            _run.AddListener(_eventListeners[i].OnRun);
            _idle.AddListener(_eventListeners[i].OnIdle);
            _jump.AddListener(_eventListeners[i].OnJump);
            _crouch.AddListener(_eventListeners[i].OnCrouch);
            _attack.AddListener(_eventListeners[i].OnAttack);
            _heavyAttack.AddListener(_eventListeners[i].OnHeavyAttack);
            _die.AddListener(_eventListeners[i].OnDie);
            _hurt.AddListener(_eventListeners[i].OnHurt);
        }
        /*switch (playerEvent)
        {
            case PlayerEventEnum.Run:
                listener.Name();
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
        }*/
    }
    
    public void AddListeners(IModelEventListener listener)
    {
        _die.AddListener(listener.OnDie);
        _hurt.AddListener(listener.OnHurt);
    }
    public void AddListeners(IAudioEventListener listener)
    {
        _heal.AddListener(listener.PlayHealAudio);
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
        _healthInfo.damage = damage;
        _healthInfo.currentHealth = currentHealth;
        if(damage < 0 ) _heal.Invoke();
        _hurt.Invoke(_healthInfo);
    }

}

public interface ISoundListener
{
}


