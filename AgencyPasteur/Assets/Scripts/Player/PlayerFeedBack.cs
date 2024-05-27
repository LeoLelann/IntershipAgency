using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerFeedBack : Player
{
    public UnityEvent OnDashing;
    public UnityEvent OnMoving;
    public UnityEvent OnThrowing;
    public UnityEvent OnDroping;
    // Start is called before the first frame update
    void Start()
    {
        OnMoving = new UnityEvent();
    }

    // Update is called once per frame
    void Update()
    {
        OnMoving.Invoke();
    }

    public void OnPlayerInteract(InputAction.CallbackContext context)
    {
        Debug.Log("Interact ");
    }
}
