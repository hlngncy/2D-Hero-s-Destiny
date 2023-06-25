using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.UI;

public class EnemyUIView : MonoBehaviour, IEnemyUIView
{
    //[SerializeField] private Slider _healthBarSlider;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private SpriteRenderer _healthSign;
    [SerializeField] private Gradient _healthGradient;

    private float _newHealth;
    public void OnHurt(HealthInfo healthInfo)
    {
        _newHealth = (float)healthInfo.currentHealth / healthInfo.maxHealth;
        DamageTextPool.Instance.GetDamageText(_enemy.transform, healthInfo.damage);
        _healthSign.color = _healthGradient.Evaluate(_newHealth);
    }

    public void OnDead()
    {
        //_playerDetected.SetActive(false);
        _healthSign.gameObject.SetActive(false);
    }

    public void OnEnemyDetect()
    {
        _healthSign.gameObject.SetActive(true);
    }

    public void OnIdle()
    {
        _healthSign.gameObject.SetActive(false);
    }

}
