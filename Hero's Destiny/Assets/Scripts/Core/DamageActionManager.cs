
using UnityEngine;

public class DamageActionManager : Singleton<DamageActionManager>
{
    public void DoDamage(Collider2D[] entities, int damage)
    {
        for (int i = 0; i < entities.Length; i++)
        {
            entities[i].GetComponent<IController>().Hurt(damage);
        }
    }
    
    public void DoDamage(Collider2D entity, int damage)
    {
        entity.GetComponent<IController>().Hurt(damage);
    }
}
