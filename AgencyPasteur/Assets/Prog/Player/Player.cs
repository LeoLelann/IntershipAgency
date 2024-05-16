using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    [SerializeField] float _dashPower = 5f;
    Glassware glassware;

    private Vector2 _movementInput;
    private bool _isGrabing;
    public bool isInRange;
    private Collider _colliders;
    public Interactable range;
    [SerializeField] private GameObject trigerGameObject;
    private void Start()
    {
        _isGrabing = false;
        isInRange = false;
    }

    void Update()
    {

        Vector3 movement = new Vector3(_movementInput.x, 0f, _movementInput.y) * _speed * Time.deltaTime;

        transform.Translate(movement, Space.World);
        transform.rotation = Quaternion.LookRotation(movement);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("fsdhjb");
        bool Interact = context.ReadValue<bool>();
        if (isInRange) //Attrape un objet
        {
            range.Interacted(gameObject);
            Debug.Log("ifed");
        }
    }

    public void OnDrop(InputAction.CallbackContext context)
    {
        if (_isGrabing)
        {
            transform.GetChild(1).parent = null;
        }
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if(transform.GetChild(1).parent != null)
        {
            GetComponentInChildren<Glassware>().Thrown();
            _isGrabing = false;
        }
    }
}
