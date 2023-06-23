using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private float count;
    [SerializeField] private TextMeshProUGUI _fps;
    
    private IEnumerator Start()
    {
        while (true)
        {
            count = 1f / Time.unscaledDeltaTime;
            _fps.text = $"fps: {Mathf.Round(count)}";
            yield return new WaitForSeconds(0.1f);
        }
    }
    
}
