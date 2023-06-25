using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BossUIView : MonoBehaviour, IEnemyUIView
{
    [SerializeField] private Slider _healthBarSlider;
    [SerializeField] private GameObject _owner;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Image _iconPlace; 

    private void Start()
    {
        _iconPlace.sprite = _icon;
    }

    public void OnHurt(HealthInfo healthInfo)
    {
        _healthBarSlider.maxValue = healthInfo.maxHealth;
        _healthBarSlider.value = (healthInfo.currentHealth - healthInfo.damage) ;
        DamageTextPool.Instance.GetDamageText(_owner.transform, healthInfo.damage);
    }

    public void OnDead()
    {
        _healthBarSlider.value = 0;
        this.gameObject.SetActive(false);
        //_healthBarSlider.transform.DOMove(transform.right * 1000, .4f).SetEase(Ease.OutBack)
        //.OnComplete(() =>this.gameObject.SetActive(false));
    }

    public void OnEnemyDetect()
    {
        Debug.Log("enemy detected boss");
        this.gameObject.SetActive(true);
    }

    public void OnIdle()
    {
        this.gameObject.SetActive(false);
    }
}
