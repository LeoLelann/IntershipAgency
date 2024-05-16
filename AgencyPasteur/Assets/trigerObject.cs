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
            Debug.Log("triggered");
            Player.isInRange = true;
            Player.range = other.transform.GetComponent<Interactable>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent("Interactable")) //Remplacer par machin d'emile
        {
            Player.isInRange = false;
            Player.range = null;
        }
    }
}
