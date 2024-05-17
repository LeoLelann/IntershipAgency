using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [HideInInspector] public Glassware glassware;
    [HideInInspector] public Interactable range;

    [HideInInspector] public bool isInRange;
    private bool _isMoving;
    private bool _isGrabing;

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
        transform.Translate(_movement, Space.World);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
        transform.rotation = Quaternion.LookRotation(_movement);
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
        if (isInRange) //Attrape un objet
        {
            range.Interacted(gameObject);
        }
    }

    public void OnDrop(InputAction.CallbackContext context)
    {
        context.ReadValue<bool>();
        if (transform.GetChild(1).parent != null)
        {
            GetComponentInChildren<Glassware>().Drop();
            _isGrabing = false;
        }
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        context.ReadValue<bool>();
        if (transform.GetChild(1).parent != null)
        {
            GetComponentInChildren<Glassware>().Thrown();
            _isGrabing = false;
        }
    }
}
