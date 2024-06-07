using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button[] _defaultBtnCanva;

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
}
