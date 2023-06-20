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

    public void OnHurt(int damage, int currentHealth, int maxHealth)
    {
        _healthBarSlider.maxValue = maxHealth;
        _healthBarSlider.value = (currentHealth - damage) ;
        DamageTextPool.Instance.GetDamageText(_owner.transform, damage);
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
