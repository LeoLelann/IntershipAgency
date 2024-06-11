using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerToken : MonoBehaviour
{
    [SerializeField] private GameObject _token;
    [SerializeField] private GameObject _parentGameObject;
    [SerializeField] private float _moveSpeed;
    private Collider _tokenCollider;
    private Vector2 _moveInput;
    private Vector3 _moveDirection;
    

    void Start()
    {
        _tokenCollider = _token.GetComponent<Collider>();
        _tokenCollider.enabled = false;
    }

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
        _moveDirection = new Vector3(_moveInput.x, _moveInput.y, 0);
        transform.position += _moveDirection * _moveSpeed * Time.deltaTime;
    }
    public void OnInteract(InputAction.CallbackContext ctx)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
        if (_token.transform.IsChildOf(this.transform) && hit.collider)
        {
            _tokenCollider.enabled = true;
            _token.transform.parent = _parentGameObject.transform;
            _token.transform.position = this.transform.position;
        }
    }
    public void OnCancel(InputAction.CallbackContext ctx)
    {
        if (_token.transform.IsChildOf(_parentGameObject.transform))
        {
            _tokenCollider.enabled=false;
            _token.transform.SetParent(this.transform);
            _token.transform.position = this.transform.position;
        }
    }
}
