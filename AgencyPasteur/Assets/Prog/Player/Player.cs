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
    private bool _isMoving;
    private bool _isGrabing;

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _playerRotation = 0.09f;
    [SerializeField] private float _dashPower = 25f;
    [SerializeField] private float _dashCD = 2f;
    [SerializeField] private float _knockback = 1f;

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
        _isGrabing = false;
        isInRange = false;
    }

    void Update()
    {
        _movement = new Vector3(_movementInput.x, 0f, _movementInput.y) * _speed * Time.deltaTime;
        transform.Translate(_movement, Space.World);
        if (_movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_movement), _playerRotation);
        }
    }
    private void OnEnable()
    {
        _input.actions["Grab"].started += OnInteract;
        //_input.actions["Grab"].canceled -= OnInteract;
        //_input.actions["Grab"].performed -= OnInteract;
        _input.actions["Dash"].started += OnDash;
        //_input.actions["Dash"].canceled -= OnDash;
        //_input.actions["Dash"].performed -= OnDash;
    }
    private void OnDisable()
    {
        _input.actions["Grab"].started -= OnInteract;
        _input.actions["Dash"].started -= OnDash;
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        StartCoroutine(Dash());
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        context.ReadValue<bool>();
        if (isInRange) //Attrape un objet
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
        _isDashing = true;
        _rb.velocity = new Vector3(transform.forward.x, 0, transform.forward.z )* _dashPower;
        yield return new WaitForSeconds(_dashCD);
        _isDashing = false;
    }

    IEnumerator Percute()
    {
        _rb.velocity = new Vector3(-transform.forward.x, 0, transform.forward.z) * _knockback;
        _rbOther.velocity = new Vector3(-transform.forward.x, 0, transform.forward.z) * _knockback;
        yield return null;
    }
}
