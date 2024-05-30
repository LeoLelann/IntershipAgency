using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UISelectionPlayer : MonoBehaviour
{
    [SerializeField] private Button _buttonP1;
    [SerializeField] private Button _buttonP2;
    [SerializeField] private Button _buttonP3;
    [SerializeField] private EventSystem _es;
    private InputAction _input;

    private void OnEnable()
    {
        
        //_es.SetSelectedGameObject(_buttonP2.gameObject);   // Defaut button on start
    }
    private void Update()
    {
        if (!_es.IsActive())
        {
            _es.SetSelectedGameObject(_buttonP1.gameObject);
            if (!_buttonP1.IsActive())
            {
                _es.SetSelectedGameObject(_buttonP2.gameObject);
                if (!_buttonP2.IsActive())
                {
                    _es.SetSelectedGameObject(_buttonP3.gameObject);
                }
            }
        }

        if (!_buttonP1.IsActive() && !_buttonP2.IsActive() && !_buttonP3.IsActive())
        {   
            
        }
    }

    private void OnClick(Button _btn)
    {
        _btn.gameObject.SetActive(false);
        //_input.actionMap.controlSchemes + ;
    }
}
