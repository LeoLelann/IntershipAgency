using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [HideInInspector] public Glassware glassware;
    [HideInInspector] public Interactable range;

    [HideInInspector] public bool isInRange { get; set; }
    [HideInInspector] public bool _isDashing { get; private set; }

    [Header("Deplacement")]
    private float _moveSpeedMax;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 0.09f;
    [SerializeField] private float _knockback = 1f;

    [Header("Dash")]
    [SerializeField] private float _dashPower = 25f;
    [SerializeField] private float _dashCD = 0.2f;
    [SerializeField] private float _dashDuration = 0.1f;
    [SerializeField] AnimationCurve _curve;
    private bool _canDash;

    [Header("")]
    private Pause _pauseMenu; 
    [SerializeField] private GameObject _bookUI;

    [SerializeField] private Rigidbody _rb;
    private Rigidbody _rbOther;
    private Vector2 _moveInput;
    private Vector3 _moveDirection;
    //private PlayerInput _playerInput;
    //private InputActionMap _movementActionMap;
    //private InputActionMap _uiActionMap;

    [SerializeField] InputActionReference _inputFromUI;
    [SerializeField] InputActionReference _inputFromGameplay;



    private void Start()
    {
        //_playerInput = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody>();
        _pauseMenu = FindObjectOfType<Pause>();
        _moveSpeedMax = _moveSpeed;
        isInRange = false;
        _isDashing = false;
        SwitchToGameplay();
        //_movementActionMap = _playerInput.actions.FindActionMap("Player");
        //_uiActionMap = _playerInput.actions.FindActionMap("UI");
    }

    void Update()
    {
        if (!_isDashing)
        {
            Move();
        }
    }
    void SwitchToGameplay()
    {
        _inputFromUI.action.actionMap.Disable();
        _inputFromGameplay.action.actionMap.Enable();
    }
    void SwitchToUI()
    {
        _inputFromUI.action.actionMap.Enable();
        _inputFromGameplay.action.actionMap.Disable();
    }
    private void Move()
    {
        //deplacement
        _moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y);
        transform.position += _moveDirection * _moveSpeed * Time.deltaTime;
        if (_moveDirection != Vector3.zero && _moveSpeed != 0f) //rotation
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_moveDirection), _rotationSpeed);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        _pauseMenu.SetPause();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (isInRange && range != null)
            {
                range.Interacted(gameObject);
                
                if (range.GetComponent<Book>()) // Interact with book
                {
                    if (!_bookUI.activeInHierarchy)
                    {
                        _moveSpeed = _moveSpeedMax;
                        SwitchToUI();
                    }
                    else
                    {
                        _moveSpeed = 0f;
                        SwitchToGameplay();
                    }

                }
            }
        }
    }

    public void OnDrop(InputAction.CallbackContext context)
    {
        if (transform.GetChild(1).parent != null && context.started)
        {
            GetComponentInChildren<Glassware>().Drop();
        }
    }
    public void OnThrow(InputAction.CallbackContext context)
    {
        if (transform.GetChild(1).parent != null && context.canceled)
        {
            GetComponentInChildren<Glassware>().Thrown();
        }
    }
    public void OnDash(InputAction.CallbackContext context)
    {
        if (/*context.performed && */!_isDashing && context.started)
        {
            StartCoroutine(Dash());
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player" && _isDashing)
        {
            _rbOther = collision.gameObject.GetComponent<Rigidbody>();
            StartCoroutine(Percute());
        }
/*        else if (_isDashing) //percuter un mur
        {
            
        }*/
    }
    /*IEnumerator Dash()
    {
        Debug.Log(_dashDuration);
        float curveValue = _curve.Evaluate(_dashPower);
        //_rb.velocity = Vector3.Lerp(transform.forward, transform.forward * _dashPower, curveValue );
        _rb.velocity += _moveDirection * Mathf.Lerp(0, _dashPower, curveValue) * Time.deltaTime;
        _dashDuration -= Time.deltaTime;
        if (_dashDuration >= 0) //dash is'nt finish
        {
            StartCoroutine(Dash());
            yield return null;
        }
        else
        {
            yield return new WaitForSeconds(_dashCD);
            _dashDuration = 0.5f;
            _isDashing = false;
        }
    }*/
    private IEnumerator Dash()
    {
        _isDashing = true;
        _canDash = false;
        Debug.Log("qgsgd");

        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + _moveDirection.normalized * _dashPower;

        float timer = 0f;
        while (timer < _dashDuration)
        {
            float t = timer / _dashDuration;
            float curveValue = _curve.Evaluate(t);
            //transform.position = Vector3.Lerp(startPosition, endPosition, curveValue);
            _rb.AddForce(Vector3.Lerp(startPosition, endPosition, curveValue));
            yield return new WaitForSeconds(Time.deltaTime);
        }

        //for (float elapsed = 0; elapsed < _dashDuration; elapsed += Time.deltaTime)
        //{
        //    float t = elapsed / _dashDuration;
        //    float curveValue = _curve.Evaluate(t);
        //    //transform.position = Vector3.Lerp(startPosition, endPosition, curveValue);
        //    _rb.AddForce(Vector3.Lerp(startPosition, endPosition, curveValue));
        //    yield return new WaitForSeconds(Time.deltaTime);
        //}

        transform.position = endPosition;

        _isDashing = false;

        yield return new WaitForSeconds(_dashCD);
        _canDash = true;
    }

    IEnumerator Percute()
    {
        _rb.velocity = new Vector3(-transform.forward.x, 0, transform.forward.z) * _knockback;
        _rbOther.velocity = new Vector3(-transform.forward.x, 0, transform.forward.z) * _knockback;
        yield return null;
    }
}
