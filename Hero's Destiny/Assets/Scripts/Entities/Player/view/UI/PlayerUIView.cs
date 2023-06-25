using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.UI;

public class PlayerUIView : MonoBehaviour, IView, IEventListener
{
    [SerializeField] private Slider _healthBarSlider;
    [SerializeField] private GameObject _player;
    [SerializeField] private Image _sliderFill;
    [SerializeField] private Gradient _healthGradient;
    public int maxHealth;
    private int _health;
    private void Start()
    {
        StartCoroutine(SliderMaxHealth());
    }

    private IEnumerator SliderMaxHealth()
    {
        yield return new WaitForEndOfFrame();
        _healthBarSlider.maxValue = maxHealth;
    }

    public void OnRun()
    {
        //TODO Add vignette effect
    }

    public void OnIdle()
    {
        //TODO Add vignette effect
    }

    public void OnJump()
    {
        //TODO Add vignette effect
    }

    public void OnCrouch(bool isCrouch)
    {
        //TODO Add vignette effect
    }

    public void OnHurt(HealthInfo healthInfo)
    {
        _health = healthInfo.currentHealth - healthInfo.damage;
        if (_health > maxHealth) _health = maxHealth;
        _healthBarSlider.value = _health ;
        _sliderFill.color = _healthGradient.Evaluate((float)_health / maxHealth);
        DamageTextPool.Instance.GetDamageText(_player.transform, healthInfo.damage);
    }

    public void OnDie()
    {
        _healthBarSlider.value = 0;
    }

    public void OnAttack()
    {
        //TODO Add vignette effect
    }

    public void OnHeavyAttack()
    {
        //TODO Add vignette effect
    }
}
