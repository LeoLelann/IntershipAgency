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
    private bool _canDash;
    [Header("Deplacement")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 0.09f;
    [SerializeField] private float _knockback = 1f;

    [Header("Dash")]
    [SerializeField] private float _dashPower = 25f;
    [SerializeField] private float _dashCD = 0.2f;
    [SerializeField] private float _dashDuration = 0.1f;
    [SerializeField] AnimationCurve _curve;

    private Rigidbody _rbOther;
    private Rigidbody _rb;
    private Vector2 _moveInput;
    private Vector3 _moveDirection;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        isInRange = false;
        _isDashing = false;
    }

    void Update()
    {

        if (!_isDashing)
        {
            Move();
        }
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
    public void OnDash(InputAction.CallbackContext context)
    {
        context.ReadValue<bool>();
        if (/*context.performed && */!_isDashing)
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
        else if (_isDashing)
        {
            //percuter un mur
        }
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

        for (float elapsed = 0; elapsed < _dashDuration; elapsed += Time.deltaTime)
        {
            Debug.Log(elapsed);
            float t = elapsed / _dashDuration;
            float curveValue = _curve.Evaluate(t);
            transform.position = Vector3.Lerp(startPosition, endPosition, curveValue);
            yield return null; // Wait until the next frame
        }

        transform.position = endPosition; // Ensure the final position is set

        _isDashing = false;

        // Wait for the cooldown period before allowing another dash
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
