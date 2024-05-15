using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 5f;

    private Vector2 _movementInput;


    void Update()
    {

        Vector3 movement = new Vector3(_movementInput.x, 0f, _movementInput.y) * speed * Time.deltaTime;

        transform.Translate(movement, Space.World);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
    }
}
