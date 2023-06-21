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
    
    private int _healthDiff;
    
    
    public void Awake()
    {
        currentHealthOB.Value = _playerData.maxHealth;
    }
    

    public void OnHurt(int damage, int currentHealth)
    {
        _healthDiff = damage;
        if (currentHealth - _healthDiff > currentHealthOB.Value) currentHealthOB.Value = _playerData.maxHealth ;
        else currentHealthOB.Value = currentHealth - _healthDiff;
    }
    
    public void OnDie()
    {
    }
}
