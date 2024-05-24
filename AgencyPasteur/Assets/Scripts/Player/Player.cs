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

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 0.09f;
    [SerializeField] private float _knockback = 1f;
    [SerializeField] private float _dashPower = 25f;
    [SerializeField] private float _dashCD = 2f;
    private float _dashTimerCD;

    [SerializeField] AnimationCurve _curve;
    private Rigidbody _rbOther;
    private Rigidbody _rb;
    private Vector2 _moveInput;
    private Vector3 _moveDirection;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        isInRange = false;
        _dashTimerCD = 0f;
        _isDashing = false;
    }

    void Update()
    {

        if (!_isDashing)
        {
            Move();
        }
        /*        if (_isDashing)
                {
                    _dashTimerCD -= Time.deltaTime;
                    if (_dashTimerCD <= 0f)
                    {
                        _isDashing = false;
                    }
                }*/
    }
    private void Move()
    {
        //deplacement
        _moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y);
        transform.position += _moveDirection * _moveSpeed * Time.deltaTime;
        if (_moveDirection != Vector3.zero) //rotation
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_moveDirection), _rotationSpeed);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
/*        context.ReadValue<bool>();
        if (*//*context.performed && *//*!_isDashing)
        {
            _isDashing = true;
            _dashTimerCD = _dashCD;
            StartCoroutine(Dash());
        }*/
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        context.ReadValue<bool>();
        if (/*context.performed && */isInRange) //Attrape un objet
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
/*    public void OnCollisionEnter(Collision collision)
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
    }*/
/*    IEnumerator Dash()
    {
        Debug.Log("started");
        
        float curveValue = _curve.Evaluate(_dashPower);
        //_rb.velocity = Vector3.Lerp(transform.forward, transform.forward * _dashPower, curveValue );
        _rb.velocity = new Vector3(transform.position.x * _dashPower, 0, transform.position.z * _dashPower);
        yield return null;
    }*/

/*    IEnumerator Percute()
    {
        _rb.velocity = new Vector3(-transform.forward.x, 0, transform.forward.z) * _knockback;
        _rbOther.velocity = new Vector3(-transform.forward.x, 0, transform.forward.z) * _knockback;
        yield return null;
    }*/
}
