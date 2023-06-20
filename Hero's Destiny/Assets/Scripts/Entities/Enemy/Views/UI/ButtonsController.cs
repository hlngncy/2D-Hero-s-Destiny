using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsController : MonoBehaviour
{
    [SerializeField] private GameObject _restart;
    [SerializeField] private GameObject _exit;
    
    public void ShowButtons()
    {
        _restart.SetActive(!_restart.activeInHierarchy);
        _exit.SetActive(!_exit.activeInHierarchy);
    }
}
