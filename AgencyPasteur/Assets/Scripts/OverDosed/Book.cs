using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Book : Interactable
{
    [SerializeField]UnityEvent _onInteractedOpen;
    [SerializeField] UnityEvent _onInteractedClose;
    [SerializeField] UnityEvent _onCanInteractFirstTime;
    [SerializeField] List<Glassware.glasswareState> _pagesState;
    [SerializeField] List<Image> _pages;
    private bool _canInteract;
    Dictionary<Glassware.glasswareState, Image> _lockedPage = new Dictionary<Glassware.glasswareState, Image>();

    [SerializeField] GameObject BookUI;

    public Dictionary<Glassware.glasswareState, Image> LockedPage { get => _lockedPage; }

    private void Start()
    {
        for (int i= 0; i < _pagesState.Count; i++)
        {
            LockedPage.Add(_pagesState[i], _pages[i]);
        }
    }
    public void BecomeInteractable()
    {
        if(!_canInteract)
        {
            _canInteract = true;
            _onCanInteractFirstTime.Invoke();
        }
        
    }
    public override void Interacted(GameObject Player)
    {
        Debug.Log("�a passe");
        if (_canInteract)
        {
            if (BookUI.activeInHierarchy)
            {
                _onInteractedClose?.Invoke();
                BookUI.SetActive(false);
            }
            else
            {
                _onInteractedOpen?.Invoke();
                BookUI.SetActive(true);
            }
        }  
    }
}
