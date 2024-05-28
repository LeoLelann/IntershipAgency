using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigerObject : MonoBehaviour
{
    Player Player;

    public void Awake()
    {
        Player = transform.parent.GetComponent<Player>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent("Interactable"))
        {
            Player.isInRange = true;
            Player.range = other.transform.GetComponent<Interactable>();
            Player.range.OnShowInteract?.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent("Interactable")) //Remplacer par machin d'emile
        {
            other.transform.GetComponent<Interactable>().OnDontShowInteract?.Invoke();
            Player.isInRange = false;
            Player.range = null;
        }
    }
}
