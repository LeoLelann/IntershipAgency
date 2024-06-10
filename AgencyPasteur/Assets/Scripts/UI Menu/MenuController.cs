using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button[] _defaultBtnCanva;

    [SerializeField] private Button[] _BackBtn;
    [SerializeField] private GameObject _MainCanva;
    [SerializeField] private GameObject _LevelCanva;
    [SerializeField] private GameObject _IntroLevelCanva;
    EventSystem _EventSystem;

    private void Start()
    {
        _EventSystem = GetComponent<EventSystem>();
    }
    private void Update()
    {
        Debug.Log(_EventSystem.gameObject.name);
    }
    public void OnBtnClick(InputAction.CallbackContext ctx)
    {
        /*        foreach (var btn in _defaultBtnCanva)
                {
                    if (btn.gameObject.activeInHierarchy)
                    {
                        btn.Select();
                    }
                }*/
        if (!_MainCanva.activeInHierarchy)
        {
            foreach (var btn in _defaultBtnCanva)
            {
                if (btn.gameObject.activeInHierarchy)
                {
                    btn.Select();
                }
            }
        }
        else
        {
            _defaultBtnCanva[0].Select();
        }
    }

    public void OnClickQuit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void OnClickBack(InputAction.CallbackContext ctx)
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
