using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerToken : MonoBehaviour
{
    [SerializeField] private GameObject _token;
    [SerializeField] private float _moveSpeed;
    private Vector2 _moveInput;
    private Vector3 _moveDirection;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        Debug.Log("Move");
        _moveInput = ctx.ReadValue<Vector2>();
        Move();
    }
    private void Move()
    {
        _moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y);
        transform.position += _moveDirection * _moveSpeed * Time.deltaTime;
    }
    public void OnInteract(InputAction.CallbackContext ctx)
    {
        Debug.Log("Interact");

        _token.transform.parent = null;
    }
    public void OnCancel(InputAction.CallbackContext ctx)
    {
        Debug.Log("cancel");

        _token.transform.SetParent(this.transform);
    }
}
