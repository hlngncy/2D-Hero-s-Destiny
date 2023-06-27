using System;
using DG.Tweening;
using TMPro;
using UnityEngine;


public class DamageText : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
    [SerializeField] private Color _healColor;
    [SerializeField] private Color _damageColor;
    private Vector3 _startScale;

    private void Update()
    {
        if (transform.parent != null)
        {
            transform.localScale = transform.parent.localScale;
        }
    }

    private void OnEnable()
    {
        _startScale = transform.localScale;
        transform.localPosition = new Vector2(0,.2f);
        transform.DOScale(_startScale * 1.3f, .3f).SetEase(Ease.OutBack).OnComplete(
            ()=> DamageTextPool.Instance.ReleaseDamageText(this,_startScale)).SetDelay(.1f);
    }

    public void ChangeText(int damage)
    {
        if (damage < 0)
        {
            text.text = $"+{Mathf.Abs(damage)}";
            text.color = _healColor;
        }
        else
        {
            text.text = $"-{damage.ToString()}";
            text.color = _damageColor;
        }
    }
}
