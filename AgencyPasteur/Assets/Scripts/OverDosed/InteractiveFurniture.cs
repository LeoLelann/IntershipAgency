using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{
    public abstract void Interacted(GameObject player);

    public UnityEvent OnShowInteract;
    public UnityEvent OnDontShowInteract;
    public UnityEvent OnStartShowInteract;

}
