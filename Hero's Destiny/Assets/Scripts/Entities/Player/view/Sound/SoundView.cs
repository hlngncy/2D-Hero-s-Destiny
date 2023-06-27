using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundView : MonoBehaviour, IView,IAudioEventListener
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioManager _audio;
    public void PlayRunAudio()
    {
        _source.clip = _audio.GetClip(EntityEventEnum.Run);
        _source.volume = .2f;
        _source.Play();
    }
    

    public void PlayJumpAudio()
    {
        _source.clip = _audio.GetClip(EntityEventEnum.Jump);
        _source.volume = .4f;
        _source.Play();
    }

    public void PlayCrouchAudio()
    {
        _source.clip = _audio.GetClip(EntityEventEnum.Crouch);
        _source.volume = .3f;
        _source.Play();
    }

    public void PlayHurtAudio()
    {
        _source.clip = _audio.GetClip(EntityEventEnum.Hurt);
        _source.volume = .3f;
        _source.Play();
    }

    public void PlayDieAudio()
    {
        _source.clip = _audio.GetClip(EntityEventEnum.Die);
        _source.volume = .4f;
        _source.Play();
    }

    public void PlayAttackAudio()
    {
        _source.clip = _audio.GetClip(EntityEventEnum.Attack);
        _source.volume = .3f;
        _source.Play();
    }

    public void PlayHeavyAttackAudio()
    {
        _source.clip = _audio.GetClip(EntityEventEnum.HeavyAttack);
        _source.volume = .2f;
        _source.Play();
    }

    public void PlayHealAudio()
    {
        _source.clip = _audio.GetClip(EntityEventEnum.Heal);
        _source.volume = .2f;
        _source.Play();
    }
}
