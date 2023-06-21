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
        transform.localPosition = Vector3.zero;
        transform.DOLocalMove(new Vector2(0,.2f), .5f).SetEase(Ease.OutExpo).OnComplete(
            ()=> DamageTextPool.Instance.ReleaseDamageText(this,_startScale));
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
            text.text = $"-{damage}";
            text.color = _damageColor;
        }
    }
}
