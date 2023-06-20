using UnityEngine;
using UnityEngine.Pool;

public class DamageTextPool : Singleton<DamageTextPool>
{
    [SerializeField] private DamageText _damageText;
    private ObjectPool<DamageText> _pool;
    private Transform _owner;
    private int _damage;
    
    
    void Start()
    {
        _pool = new ObjectPool<DamageText>(
            () => { return Instantiate(_damageText); },
            GetDamageTextFromPool,
            ReleaseDamageTextFromPool,
            text => { Destroy(text.gameObject); }, 
            false, 
            10,
            20);
        _pool.Get();
    }

    private void GetDamageTextFromPool(DamageText obj)
    {
        obj.ChangeText(_damage);
        obj.transform.SetParent(_owner);
        obj.transform.position = Vector3.zero;
        obj.gameObject.SetActive(true);
        
    }
    
    private void ReleaseDamageTextFromPool(DamageText obj)
    {
        obj.gameObject.SetActive(false);
    }

    public void GetDamageText(Transform owner, int damage)
    {
       _owner = owner;
       _damage = damage;
       _pool.Get();
    }

    public void ReleaseDamageText(DamageText text, Vector3 startscale)
    {
        _pool.Release(text);
        text.transform.SetParent(transform);
        text.transform.localScale = Vector3.one;
    }
    
}
