using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundView : MonoBehaviour, IView
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioManager _audio;
    public void PlayRunAudio()
    {
        _source.clip = _audio.GetClip(PlayerEventEnum.Run);
        _source.volume = .3f;
        _source.Play();
    }
    

    public void PlayJumpAudio()
    {
        _source.clip = _audio.GetClip(PlayerEventEnum.Jump);
        _source.Play();
    }

    public void PlayCrouchAudio()
    {
        _source.clip = _audio.GetClip(PlayerEventEnum.Crouch);
        _source.Play();
    }

    public void PlayHurtAudio()
    {
        _source.clip = _audio.GetClip(PlayerEventEnum.Hurt);
        _source.Play();
    }

    public void PlayDieAudio()
    {
        _source.clip = _audio.GetClip(PlayerEventEnum.Die);
        _source.Play();
    }

    public void PlayAttackAudio()
    {
        _source.clip = _audio.GetClip(PlayerEventEnum.Attack);
        _source.Play();
    }

    public void PlayHeavyAttackAudio()
    {
        _source.clip = _audio.GetClip(PlayerEventEnum.HeavyAttack);
        _source.Play();
    }
}
