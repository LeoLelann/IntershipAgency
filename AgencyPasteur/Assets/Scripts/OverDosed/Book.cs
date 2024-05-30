using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Book : Interactable
{
    [SerializeField]UnityEvent OnInteractedOpen;
    [SerializeField] UnityEvent OnInteractedClose;

    [SerializeField] GameObject BookUI;
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
