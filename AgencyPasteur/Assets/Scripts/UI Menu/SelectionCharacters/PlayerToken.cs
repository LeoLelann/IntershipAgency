using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerToken : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Sprite _spriteCadre;
    [SerializeField] private Sprite _dftSpriteCadre;
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
        TokenOnSelect tampon=_currentSelection;
        _currentSelection.GetComponentInChildren<Image>().sprite = _spriteCadre;
        _currentSelection.GetComponent<BoxCollider2D>().enabled = false;
        _currentSelection = tampon;
        _isChoosed = true;
    }
    public void OnCancel(InputAction.CallbackContext ctx)
    {
        _onChoosedCancelled.Invoke();
        Debug.Log(_currentSelection.name);
        _currentSelection.GetComponentInChildren<Image>().sprite = _dftSpriteCadre;
        _currentSelection.GetComponent<BoxCollider2D>().enabled =true;
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
