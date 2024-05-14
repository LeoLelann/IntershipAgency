using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 5f;

    private Vector2 movementInput;


    void Update()
    {

        Vector3 movement = new Vector3(movementInput.x, 0f, movementInput.y) * speed * Time.deltaTime;

        transform.Translate(movement, Space.World);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
}
