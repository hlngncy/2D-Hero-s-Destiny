using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip _run;
    [SerializeField] private AudioClip _attack;
    [SerializeField] private AudioClip _heavyAttack;
    [SerializeField] private AudioClip _jump;
    [SerializeField] private AudioClip _crouch;
    [SerializeField] private AudioClip _die;
    [SerializeField] private AudioClip _hurt;
    [SerializeField] private AudioClip _heal;

    public AudioClip GetClip(EntityEventEnum entityEvent)
    {
        switch (entityEvent)
        {
            case  EntityEventEnum.Run:
                return _run;
            case  EntityEventEnum.Attack:
                return _attack;
            case  EntityEventEnum.Jump:
                return _jump;
            case  EntityEventEnum.Crouch:
                return _crouch;
            case  EntityEventEnum.HeavyAttack:
                return _heavyAttack;
            case  EntityEventEnum.Hurt:
                return _hurt;
            case  EntityEventEnum.Die:
                return _die;
            case  EntityEventEnum.Heal:
                return _heal;
            default:
                return null;
        }
        return null;
    }
}
