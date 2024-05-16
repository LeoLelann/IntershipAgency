using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    [SerializeField] float _dashPower = 5f;

    private Vector2 _movementInput;
    private bool _isGrabing;
    private Collider _colliders;

    private void Start()
    {
        _isGrabing = false;
    }

    void Update()
    {

        Vector3 movement = new Vector3(_movementInput.x, 0f, _movementInput.y) * _speed * Time.deltaTime;

        transform.Translate(movement, Space.World);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("")) //Remplacer par machin d'emile
        {
            //bool ca mere true
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("")) //Remplacer par machin d'emile
        {
            //bool ca mere false
        }
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
        if (!_isGrabing /* &&  bool ca mere */ )
        {
            //Machin de emile encore && isGrabing true
        }
        else if (_isGrabing /* &&  infrontof machine*/)
        {
            //mettre le truc dans la machine
        }
        else if (_isGrabing)
        {
            transform.GetChild(1).parent = null;
        }
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        //machin d'emile
        _isGrabing = false;
    }
}
