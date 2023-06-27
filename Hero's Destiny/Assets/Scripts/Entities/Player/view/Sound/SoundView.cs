using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundView : MonoBehaviour, IView
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioManager _audio;
    public void Run()
    {
        _source.clip = _audio.GetClip(PlayerEventEnum.Run);
        _source.volume = .3f;
        _source.Play();
    }
    

    public void Jump()
    {
        _source.clip = _audio.GetClip(PlayerEventEnum.Jump);
        _source.Play();
    }

    public void Crouch()
    {
        _source.clip = _audio.GetClip(PlayerEventEnum.Crouch);
        _source.Play();
    }

    public void Hurt()
    {
        _source.clip = _audio.GetClip(PlayerEventEnum.Hurt);
        _source.Play();
    }

    public void Die()
    {
        _source.clip = _audio.GetClip(PlayerEventEnum.Die);
        _source.Play();
    }

    public void Attack()
    {
        _source.clip = _audio.GetClip(PlayerEventEnum.Attack);
        _source.Play();
    }

    public void HeavyAttack()
    {
        _source.clip = _audio.GetClip(PlayerEventEnum.HeavyAttack);
        _source.Play();
    }
}
