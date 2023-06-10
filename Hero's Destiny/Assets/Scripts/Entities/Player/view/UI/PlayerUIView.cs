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
    }

    public void OnIdle()
    {
    }

    public void OnJump()
    {
    }

    public void OnCrouch(bool isCrouch)
    {
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
    }

    public void OnHeavyAttack()
    {
    }
}
