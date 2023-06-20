using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.UI;

public class PlayerUIView : MonoBehaviour, IView, IEventListener
{
    [SerializeField] private Slider _healthBarSlider;
    [SerializeField] private GameObject _player;
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
        _healthBarSlider.value = (currentHealth - damage) ;
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
