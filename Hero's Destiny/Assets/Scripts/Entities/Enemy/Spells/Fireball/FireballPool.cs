using UnityEngine;
using UnityEngine.Pool;

public class FireballPool : MonoBehaviour
{
    [SerializeField] private Fireball _fireball;
    private ObjectPool<Fireball> _pool;
    private Vector3 _position;
    private Quaternion _rotation;
    public int _damage;


    void Start()
    {
        _pool = new ObjectPool<Fireball>(
            () => { return Instantiate(_fireball,_position, _rotation); },
            GetFireballFromPool,
            ReleaseFireballFromPool,
            fireball => { Destroy(fireball.gameObject); }, 
            false, 
            10,
            15);
    }

    private void GetFireballFromPool(Fireball obj)
    {
        obj.transform.position = _position;
        obj.transform.rotation = _rotation;
        obj.gameObject.SetActive(true);
    }
    
    private void ReleaseFireballFromPool(Fireball obj)
    {
        obj.gameObject.SetActive(false);
    }

    public void GetFireball(Vector3 position, Quaternion rotation, int damage)
    {
        _position = position;
        _rotation = rotation;
        _damage = damage;
        _pool.Get();
    }

    public void ReleaseFireball(Fireball fireball, Collider2D player = null)
    {
        if (player != null)
        {
            DamageActionManager.Instance.DoDamage(player, _damage);
        }
        _pool.Release(fireball);
    }

}
