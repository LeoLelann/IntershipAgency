using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveTest : MonoBehaviour
{

    [HideInInspector] public Glassware glassware;
    [HideInInspector] public Interactable range;

    [HideInInspector] public bool isInRange;
    [HideInInspector] public bool _isDashing;
    [HideInInspector] public bool _canDash;

    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 0.07f;
    [SerializeField] private float _knockback = 1f;
    [Header("Dash")]
    [SerializeField] private float _dashSpeed = 25f;
    [SerializeField] private float _dashCooldown = 2f;
    [SerializeField] private float _dashDuration = 0.2f;

    private Vector2 _moveInput;
    private Vector3 _moveDirection;

    [SerializeField] AnimationCurve _curve;

    private void Start()
    {
        isInRange = false;
        _canDash = true;
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
        if (/*context.performed && */isInRange) //Attrape un objet
        {
            range.Interacted(gameObject);
        }
    }
    public void OnDrop(InputAction.CallbackContext context)
    {
        if (transform.GetChild(1).parent != null)
        {
            GetComponentInChildren<Glassware>().Drop();
        }
    }
    public void OnThrow(InputAction.CallbackContext context)
    {
        if (transform.GetChild(1).parent != null)
        {
            GetComponentInChildren<Glassware>().Thrown();
        }
    }
    public void OnDash(InputAction.CallbackContext context)
    {
        Debug.Log("OnDash");

        if (context.started && _canDash /*&& _moveDirection != Vector3.zero*/)
        {
            Debug.Log("utqdrsdyfvgbd");
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        Debug.Log("Corroutinedesesmorts");

        _isDashing = true;
        _canDash = false;

        float startTime = Time.time;

        while (Time.time < startTime + _dashDuration)
        {
            Debug.Log("pfffiuuu");

            transform.position += _moveDirection * _dashSpeed * Time.deltaTime;
            yield return null;
        }

        _isDashing = false;

        // Wait for the cooldown period before allowing another dash
        yield return new WaitForSeconds(_dashCooldown);
        _canDash = true;
    }
}
