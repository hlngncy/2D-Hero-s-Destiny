
using System.Collections;

using UnityEngine;


public class Fireball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private FireballPool _pool;
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.CompareTag("Player"))
            _pool.ReleaseFireball(this, col.collider);
        else
            _pool.ReleaseFireball(this);
    }

    private void OnEnable()
    {
        rb.AddForce(transform.right * 500);
        StartCoroutine(Release());
    }

    private IEnumerator Release()
    {
        yield return new WaitForSeconds(5);
        if(enabled) _pool.ReleaseFireball(this);
    }
}
