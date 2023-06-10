using System;
using DG.Tweening;
using TMPro;
using UnityEngine;


public class DamageText : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
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
        transform.DOLocalMove(new Vector2(0,.2f), .3f).SetEase(Ease.OutExpo).OnComplete(
            ()=> DamageTextPool.Instance.ReleaseDamageText(this,_startScale));
    }

    public void ChangeText(int damage)
    {
        text.text = $"-{damage}";
    }
}
