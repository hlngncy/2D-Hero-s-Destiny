using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.UI;

public class EnemyUIView : MonoBehaviour, IEnemyUIView
{
    //[SerializeField] private Slider _healthBarSlider;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _playerDetected;

    public void OnHurt(int damage)
    {
        DamageTextPool.Instance.GetDamageText(_enemy.transform, damage);
    }

    public void OnDead()
    {
        _playerDetected.SetActive(false);
    }

    public void OnEnemyDetect()
    {
        _playerDetected.SetActive(true);
    }

    public void OnIdle()
    {
        _playerDetected.SetActive(false);
    }

}
