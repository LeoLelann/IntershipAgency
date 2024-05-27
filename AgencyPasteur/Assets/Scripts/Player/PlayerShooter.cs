using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerShooter : MonoBehaviour
{
    private CinemachineTargetGroup group;
    [HideInInspector] public bool _isDashing;
    [HideInInspector] public bool _isShooting;
    [HideInInspector] public bool _isStopping;
    private bool _isMoving;

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _playerRotation = 0.09f;
    [SerializeField] private float _dashPower = 25f;
    [SerializeField] private float _dashCD = 2f;
    [SerializeField] private float _dispersion=0.1f;
     private float _cdShoot=0;
    [SerializeField] weapon _weapon;
    [SerializeField] GameObject _projectile;

    private enum weapon
    {
        GUN
    };

    [SerializeField] AnimationCurve _curve;
    private PlayerInput _input;
    private Rigidbody _rbOther;
    private Rigidbody _rb;
    private Vector2 _movementInput;
    private Vector3 _movement;

    private void Awake()
    {
        group = FindObjectOfType<CinemachineTargetGroup>();
        Transform cameraPosition = transform;
        cameraPosition.rotation = new Quaternion(90, 0, 0, 0);
        group.AddMember(cameraPosition,1,10);
    }
    private void Start()
    {
        _weapon = weapon.GUN;
        _rb = GetComponent<Rigidbody>();
        _input = GetComponent<PlayerInput>();
        _input.actions["Grab"].started+= OnInteract;
        _input.actions["Grab"].canceled += OnCancelShoot;
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

        //_input.actions["Grab"].canceled -= OnInteract;
        //_input.actions["Grab"].performed -= OnInteract;
        //_input.actions["Dash"].started += OnDash;
        //_input.actions["Dash"].canceled -= OnDash;
        //_input.actions["Dash"].performed -= OnDash;
        
    }
    private void OnDisable()
    {
        //_input.actions["Dash"].started -= OnDash;
        //_input.actions["Interact"].performed -= OnInteract;

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
        Debug.Log(context.phase);
        if (context.started)
        {
            switch (_weapon)
            {
                case weapon.GUN:
                    _cdShoot = 0.3f;
                    if (!_isShooting)
                    {
                        StartCoroutine(Shoot(_cdShoot));
                    }
                    break;
            }
        }    
    }
    public void OnCancelShoot(InputAction.CallbackContext context)
    {
        if (_isShooting&&!_isStopping)
        {
            StopAllCoroutines();
            StartCoroutine(StopShooting(_cdShoot));
        }
    }
    IEnumerator StopShooting(float cooldown)
    {
        _isStopping = true;
        yield return new WaitForSeconds(cooldown);        
        _isShooting = false;
        _isStopping = false;
    }
    IEnumerator Shoot(float cooldown)
    {
        _isShooting = true;
        float rand = Random.Range(-_dispersion,_dispersion);
        GameObject projectile=Instantiate(_projectile,transform.localPosition+transform.forward,transform.rotation);
        if (projectile.GetComponent<Projectile>().Speed < _speed)
        {
            projectile.GetComponent<Projectile>().Speed = _speed;
        }
        yield return new WaitForSeconds(cooldown);
        StartCoroutine(Shoot(cooldown));
    }
    IEnumerator Dash()
    {
        Debug.Log("started");
        _isDashing = true;
        _rb.velocity = new Vector3(transform.forward.x, 0, transform.forward.z) * _dashPower;
        yield return new WaitForSeconds(_dashCD);
        _isDashing = false;
    }
}
