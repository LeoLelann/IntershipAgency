using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // activer UI montrant qu'on peut ramasser avec un bouton au dessus du parent
        }
    }
}
