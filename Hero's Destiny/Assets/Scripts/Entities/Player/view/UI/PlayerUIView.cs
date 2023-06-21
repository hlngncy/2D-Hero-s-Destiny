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

    public void OnHurt(int damage, int currentHealth)
    {
        int health = currentHealth - damage;
        if (health > maxHealth) health = maxHealth;
        _healthBarSlider.value = health ;
        Debug.Log(health);
        Debug.Log(_healthBarSlider.value);
        _sliderFill.color = _healthGradient.Evaluate((float)health / maxHealth);
        DamageTextPool.Instance.GetDamageText(_player.transform, damage);
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
