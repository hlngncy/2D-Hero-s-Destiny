
using UnityEngine;

public class PlayerModel : MonoBehaviour, IModel, IModelEventListener
{
    

    //properties
    public Observable<int> CurrentHealth => currentHealthOB;
    public int JumpPower => _playerData.jumpPower;
    public int MovementSpeed => _playerData.movementSpeed;
    public int AttackRange => _playerData.attackRange;
    public int AttackPower => _playerData.attackPower;
    public int HeavyAttackPower => _playerData.heavyAttackPower;
    public float AttackCooldown => _playerData.attackCooldown;
    public float HeavyAttackCooldown => _playerData.heavyAttackCooldown;
    private Observable<int> currentHealthOB = new Observable<int>();
    
    //fields
    [SerializeField] private PlayerSO _playerData;
    [SerializeField] private PlayerEvents _events;
    private int _healthDiff;
    public void Awake()
    {
        if (currentHealthOB == null)
        {
            Debug.Log("null");
            _playerData = Instantiate(_playerData);
        }
        currentHealthOB.Value= _playerData.maxHealth;
    }

    private void Start()
    {
        _events.AddListeners(this, PlayerEventEnum.Die);
        _events.AddListeners(this, PlayerEventEnum.Hurt);
    }

    public void OnHurt(int damage, int currentHealth)
    {
        _healthDiff = damage;
        currentHealthOB.Value =- _healthDiff;
    }
    
    public void OnDie()
    {
        Debug.Log("dead");
    }
}
