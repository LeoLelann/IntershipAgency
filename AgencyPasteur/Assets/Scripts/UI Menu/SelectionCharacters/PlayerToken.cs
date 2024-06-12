using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerToken : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    [SerializeField] UnityEvent _onChoosed;
    [SerializeField] UnityEvent _onChoosedCancelled;

    [SerializeField] TokenOnSelect _currentSelection;

    private Vector2 _moveInput;
    public bool _isChoosed { get; set; }
    public TokenOnSelect CurrentSelection { get => _currentSelection; }

    void Update()
    {
        Move();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();
    }
    private void Move()
    {
        if (_isChoosed) return;

        Vector3 _moveDirection = new Vector3(_moveInput.x, _moveInput.y, 0);
        transform.position += _moveDirection * _moveSpeed * Time.deltaTime;
    }
    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (_currentSelection == null) return;

        _onChoosed.Invoke();
        _isChoosed = true;
    }
    public void OnCancel(InputAction.CallbackContext ctx)
    {
        _onChoosedCancelled.Invoke();
        _isChoosed = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<TokenOnSelect>(out var tos))
        {
            _currentSelection = tos;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<TokenOnSelect>(out var tos) && _currentSelection == tos)
        {
            _currentSelection = null;
        }
    }


}
