using DG.Tweening;
using TMPro;
using UnityEngine;


public class DamageText : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
    
    private void OnEnable()
    {
        Vector3 startscale = transform.localScale;
        transform.localPosition = Vector3.zero;
        transform.DOLocalMove(new Vector2(0,.2f), .3f).SetEase(Ease.OutExpo).OnComplete(
            ()=> DamageTextPool.Instance.ReleaseDamageText(this,startscale));
    }

    public void ChangeText(int damage)
    {
        text.text = $"-{damage}";
    }

}
