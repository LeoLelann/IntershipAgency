using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void Interacted(GameObject player);

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // activer UI montrant qu'on peut ramasser avec un bouton 
        }
    }

}
