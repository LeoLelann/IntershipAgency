using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button[] _backButtons;

    [SerializeField] private Canvas _mainCanvas;
    [SerializeField] private Canvas _levelCanvas;
    [SerializeField] private Canvas _introCanvas;

    [SerializeField] private Button _defaultMainButton;
    [SerializeField] private Button _defaultLevelButton;
    [SerializeField] private Button _defaultIntroButton;

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        }
        else if (EventSystem.current.currentSelectedGameObject == null)
        {
            ChangeEventSelectedBtn();
        }
    }

    public void ChangeEventSelectedBtn()
    {
        if (_mainCanvas.gameObject.activeInHierarchy)
        {
            OnChangeToMainCanvas();
        }
        else if (_levelCanvas.gameObject.activeInHierarchy)
        {
            OnChangeToLevelCanvas();
        }
        else if (_introCanvas.gameObject.activeInHierarchy)
        {
            OnChangeToIntroCanvas();
        }
    }

    public void OnChangeToMainCanvas()
    {
        SetSelectedButton(_defaultMainButton);
    }

    public void OnChangeToLevelCanvas()
    {
        SetSelectedButton(_defaultLevelButton);
    }

    public void OnChangeToIntroCanvas()
    {
        SetSelectedButton(_defaultIntroButton);
    }

    private void SetSelectedButton(Button button)
    {
        if (button != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(button.gameObject);
        }
    }

    public void OnButtonClick(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            var currentButton = EventSystem.current.currentSelectedGameObject?.GetComponent<Button>();
            if (currentButton != null)
            {
                currentButton.onClick.Invoke();
                Debug.Log(currentButton.name);
            }
        }
    }

    public void OnClickQuit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void OnClickBack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Back");
            foreach (var button in _backButtons)
            {
                if (button.gameObject.activeInHierarchy)
                {
                    button.onClick.Invoke();
                }
            }
        }
    }
}