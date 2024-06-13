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
    private GameObject _gameObject;
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

    private void OnClick(InputAction.CallbackContext ctx)
    {
        this.gameObject.SetActive(false);
        Debug.Log(this.name);
        _gameObject = this.gameObject;
        //_input.actionMap.controlSchemes + ;
    }

    private void OnCancel(InputAction.CallbackContext ctx)
    {
        _gameObject.gameObject.SetActive(true);
    }
}
