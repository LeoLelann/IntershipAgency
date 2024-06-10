using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button[] _defaultBtnCanva;

    [SerializeField] private Button[] _BackBtn;

    public void OnBtnClick(InputAction.CallbackContext ctx)
    {
        foreach (var btn in _defaultBtnCanva)
        {
            if (btn.gameObject.activeInHierarchy)
            {
                btn.Select();
            }
        }
    }

    public void OnClickQuit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void OnClickBack()
    {
        foreach (var btn in _BackBtn)
        {
            if (btn.gameObject.activeInHierarchy)
            {
                btn.onClick.Invoke();
            }
        }
    }
}
