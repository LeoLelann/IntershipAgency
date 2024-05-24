using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    [HideInInspector] public Glassware glassware;
    [HideInInspector] public Interactable range;

    [HideInInspector] public bool isInRange;
    private bool _isMoving;
    private bool _isGrabing;
    [SerializeField] AnimationCurve _animation;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _dashPower = 5f;

    private Rigidbody _rb;
    private Vector2 _movementInput;
    private Vector3 _movement;

    private void Start()
    {
        _isGrabing = false;
        isInRange = false;
    }

    void Update()
    {
        _movement = new Vector3(_movementInput.x, 0f, _movementInput.y) * _speed * Time.deltaTime;
        transform.position += _movement;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(_movement),2);
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        Debug.Log("OnDash");
        context.ReadValue<bool>();
        _rb.AddForce(transform.forward * _dashPower);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        context.ReadValue<bool>();
        
    }
}
