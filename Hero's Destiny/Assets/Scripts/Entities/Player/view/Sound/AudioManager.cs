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

    public AudioClip GetClip(PlayerEventEnum playerEvent)
    {
        switch (playerEvent)
        {
            case  PlayerEventEnum.Run:
                return _run;
            case  PlayerEventEnum.Attack:
                return _attack;
            case  PlayerEventEnum.Jump:
                return _jump;
            case  PlayerEventEnum.Crouch:
                return _crouch;
            case  PlayerEventEnum.HeavyAttack:
                return _heavyAttack;
            case  PlayerEventEnum.Hurt:
                return _hurt;
            case  PlayerEventEnum.Die:
                return _die;
            default:
                return null;
        }
        return null;
    }
}
