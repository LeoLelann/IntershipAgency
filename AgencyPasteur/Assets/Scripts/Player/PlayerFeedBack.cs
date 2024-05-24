using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerFeedBack : MonoBehaviour
{
    public UnityEvent testEvents;
    // Start is called before the first frame update
    void Start()
    {
        testEvents = new UnityEvent();
    }

    // Update is called once per frame
    void Update()
    {
        testEvents.Invoke();
    }

    public void OnPlayerInteract(InputAction.CallbackContext context)
    {
        Debug.Log("Interact ");
    }
}
