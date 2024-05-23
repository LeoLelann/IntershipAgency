using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [HideInInspector] public Glassware glassware;
    [HideInInspector] public Interactable range;

    [HideInInspector] public bool isInRange;
    [HideInInspector] public bool _isDashing;

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _playerRotation = 0.09f;
    [SerializeField] private float _knockback = 1f;
    [SerializeField] private float _dashPower = 25f;
    [SerializeField] private float _dashCD = 2f;
    private float _dashTimerCD;

    [SerializeField] AnimationCurve _curve;
    private PlayerInput _input;
    private Rigidbody _rbOther;
    private Rigidbody _rb;
    private Vector2 _movementInput;
    private Vector3 _movement;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _input = GetComponent<PlayerInput>();
        isInRange = false;
        _dashTimerCD = 0f;
    }

    void Update()
    {
        _movement = new Vector3(_movementInput.x, 0f, _movementInput.y) * _speed * Time.deltaTime;
        transform.Translate(_movement, Space.World);
        if (_movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_movement), _playerRotation);
        }
        if (_isDashing)
        {
            _dashTimerCD -= Time.deltaTime;
            if (_dashTimerCD <= 0f)
            {
                _isDashing = false;
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && !_isDashing)
        {
            _isDashing = true;
            _dashTimerCD = _dashCD;
            StartCoroutine(Dash());
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        context.ReadValue<bool>();
        if (isInRange && context.performed) //Attrape un objet
        {
            Debug.Log(range);
            range.Interacted(gameObject);
        }
    }

    public void OnDrop(InputAction.CallbackContext context)
    {
        context.ReadValue<bool>();
        if (transform.GetChild(1).parent != null)
        {
            GetComponentInChildren<Glassware>().Drop();
        }
    }
    public void OnThrow(InputAction.CallbackContext context)
    {
        context.ReadValue<bool>();
        if (transform.GetChild(1).parent != null)
        {
            GetComponentInChildren<Glassware>().Thrown();
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player" && _isDashing)
        {
            _rbOther = collision.gameObject.GetComponent<Rigidbody>();
            StartCoroutine(Percute());
        }
        else if(_isDashing)
        {
            //percuter un mur
        }
    }
    IEnumerator Dash()
    {
        Debug.Log("started");
        
        float curveValue = _curve.Evaluate(_dashPower);
        _rb.velocity = Vector3.Lerp(transform.forward, transform.forward * _dashPower, curveValue );
        return null;
    }

    IEnumerator Percute()
    {
        _rb.velocity = new Vector3(-transform.forward.x, 0, transform.forward.z) * _knockback;
        _rbOther.velocity = new Vector3(-transform.forward.x, 0, transform.forward.z) * _knockback;
        yield return null;
    }
}
