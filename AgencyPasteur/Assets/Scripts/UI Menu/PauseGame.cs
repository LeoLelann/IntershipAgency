using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject _pauseCanva;
    [SerializeField] private GameObject _defaultPauseBtn;
    private EventSystem _es;
    [HideInInspector] public bool isPause {get ; private set;}

    void Start()
    {
        isPause = false;
        _pauseCanva.SetActive(false);
        //_es = GetComponent<EventSystem>();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (_pauseCanva.activeInHierarchy)
            {
                isPause = true;
                _pauseCanva.SetActive(true);
                //_es.firstSelectedGameObject = _defaultPauseBtn;
            }
            else
            {
                isPause = false;
                _pauseCanva.SetActive(false);
            }
        }
    }

    public void ContinueGame()
    {
        _pauseCanva?.SetActive(false);
    }
}
