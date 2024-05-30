using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Book : Interactable
{
    [SerializeField]UnityEvent OnInteractedOpen;
    [SerializeField] UnityEvent OnInteractedClose;
    [SerializeField] List<Glassware.glasswareState> _pagesState;
    [SerializeField] List<Image> _pages;
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
    public override void Interacted(GameObject Player)
    {
        if (BookUI.activeInHierarchy)
        {
            OnInteractedClose?.Invoke();
            BookUI.SetActive(false);
        }
        else
        {
            OnInteractedOpen?.Invoke();
            BookUI.SetActive(true);
        }   
    }
}
