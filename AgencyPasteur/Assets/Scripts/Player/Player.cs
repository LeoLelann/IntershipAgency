using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class Player : MonoBehaviour
{
    [HideInInspector] public Glassware glassware;
    [SerializeField] public Interactable range;

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
    private GameObject _bookUI;

    [SerializeField] private Rigidbody _rb;
    private Rigidbody _rbOther;
    private Vector2 _moveInput;
    private Vector3 _moveDirection;
    //private PlayerInput _playerInput;
    //private InputActionMap _movementActionMap;
    //private InputActionMap _uiActionMap;

    [Header("UI")]
    [SerializeField] private InputActionReference _inputFromUI;
    [SerializeField] private InputActionReference _inputFromGameplay;
    [SerializeField] private Button[] _bookComposantBtnR;
    [SerializeField] private Button[] _bookComposantBtnL;
    [SerializeField] private Button[] _bookMachineBtnR;
    [SerializeField] private Button[] _bookMachineBtnL;
    [SerializeField] private Button[] _bookSolutionBtnR;
    [SerializeField] private Button[] _bookSolutionBtnL;
    private Button[] _bookPageR;
    private Button[] _bookPageL;
    [SerializeField] private Button[] _bookChapBtnR;
    [SerializeField] private Button[] _bookChapBtnL;
    [SerializeField] private GameObject _panelBook1;
    [SerializeField] private GameObject _panelBook2;
    [SerializeField] private GameObject _panelBook3;
    private bool _isActivePage;
    private int _currentPage;

    [SerializeField] private GameObject _pauseCanva;
    [HideInInspector] public bool isPause { get; private set; }

    [Header("Events")]
    [SerializeField] private UnityEvent _onMove;
    [SerializeField] private UnityEvent _onInteract;
    [SerializeField] private UnityEvent _onDrop;
    [SerializeField] private UnityEvent _onThrow;
    [SerializeField] private UnityEvent _onDash;

    static event Action OnPauseGlobal;
    static event Action OnUnPauseGlobal;

    private void Awake()
    {
        _bookUI = GameObject.FindGameObjectWithTag("BookUI");
        
        OnPauseGlobal += PauseTrigger;
        OnUnPauseGlobal += UnpauseTrigger;
    }
    private void OnDestroy()
    {
        OnPauseGlobal -= PauseTrigger;
        OnUnPauseGlobal -= UnpauseTrigger;
    }

    void PauseTrigger()
    {
        GetComponent<PlayerInput>().SwitchCurrentActionMap(_inputFromUI.action.actionMap.name);
    }

    void UnpauseTrigger()
    {
        GetComponent<PlayerInput>().SwitchCurrentActionMap(_inputFromGameplay.action.actionMap.name);
    }

    private void Start()
    {
        //_playerInput = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody>();
        _pauseMenu = FindObjectOfType<Pause>();
        _bookUI.SetActive(false);
        _moveSpeedMax = _moveSpeed;
        _pauseCanva.SetActive(false);
        _bookPageR = _bookComposantBtnR;
        _bookPageL = _bookComposantBtnL;
        _isActivePage = true;
        isInRange = false;
        _isDashing = false;
        _currentPage = 1;
        //_movementActionMap = _playerInput.actions.FindActionMap("Player");
        //_uiActionMap = _playerInput.actions.FindActionMap("UI");
    }

    void Update()
    {
        if (!_isDashing && isPause == false)
        {
            Move();
        }

        if (_pauseCanva.activeInHierarchy)
        {
            _moveSpeed = 0f;
            isPause = true;
        }
        else if (!_pauseCanva.activeInHierarchy)
        {
            _moveSpeed = _moveSpeedMax;
            isPause = false;
        }
        //for (int i = 0; i < _bookPageR.Length; i++)
        //{
        //    Debug.Log(_bookPageR[i].gameObject.name);
        //}
        
    }
    
    private void Move()
    {
        _onMove?.Invoke();
        //deplacement
        _moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y);
        transform.position += _moveDirection * _moveSpeed * Time.deltaTime;
        if (_moveDirection != Vector3.zero && _moveSpeed != 0f && isPause == false) //rotation
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
        //_pauseMenu.SetPause();
        if (context.started)
        {
            Debug.Log("startInput");
            if (!_pauseCanva.activeInHierarchy)
            {
                Debug.Log("activeUI");
                isPause = true;

                OnPauseGlobal?.Invoke();

                _pauseCanva.SetActive(true);
                //_es.firstSelectedGameObject = _defaultPauseBtn;
            }
            else
            {
                isPause = false;
                _pauseCanva.SetActive(false);
                OnUnPauseGlobal?.Invoke();

            }
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started )
        {
            _onInteract?.Invoke();
            if (isInRange && range != null && isPause == false)
            {
                range.Interacted(gameObject);
                
                if (range.GetComponent<Book>()) // Interact with book
                {
                    if (!_bookUI.activeInHierarchy)
                    {
                        //_moveSpeed = _moveSpeedMax;
                        UnpauseTrigger();
                    }
                    else
                    {
                        //_moveSpeed = 0f;
                        PauseTrigger();
                    }

                }
            }
        }
    }

    public void OnDrop(InputAction.CallbackContext context)
    {
        if (transform.GetChild(1).parent != null && context.started && isPause == false)
        {
            _onDrop?.Invoke();
            GetComponentInChildren<Glassware>().Drop();
        }
    }
    public void OnThrow(InputAction.CallbackContext context)
    {
        if (transform.GetChild(1).parent != null && context.canceled && isPause == false)
        {
            _onThrow?.Invoke();
            GetComponentInChildren<Glassware>()?.Thrown();
        }
    }
    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started && !_isDashing && context.started && isPause == false)
        {
            _onDash?.Invoke();
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

    public void OnNavigateChap(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            float ctxValue = context.ReadValue<float>();
            _isActivePage = false;
            if (ctxValue < -0.5f)
            {
                if (_currentPage == 1)
                {
                    return;
                }
                else if (_currentPage == 2)
                {
                    _bookChapBtnL[0].onClick.Invoke();
                    _currentPage--;
                }
                else if (_currentPage == 3)
                {
                    _bookChapBtnL[1].onClick.Invoke();
                    _currentPage--;
                }
            }
            else if (ctxValue > 0.5f)
            {
                if (_currentPage == 1)
                {
                    _bookChapBtnR[0].onClick.Invoke();
                    _currentPage++;
                }
                else if (_currentPage == 2)
                {
                    _bookChapBtnR[1].onClick.Invoke();
                    _currentPage++;
                }
                else if (_currentPage == 3)
                {
                    return;
                }
            }
        }
    }
    public void OnNavigateHorizontal(InputAction.CallbackContext context)
    {//Book change page
        if (context.started)
        {
            float ctxValue = context.ReadValue<float>();

            if (!_isActivePage)
            {
                if (_panelBook1.activeInHierarchy && !_isActivePage)
                {
                    _bookPageR = _bookComposantBtnR;
                    _bookPageL = _bookComposantBtnL;
                    _isActivePage = true;
                }
                if (_panelBook2.activeInHierarchy && !_isActivePage)
                {
                    _bookPageR = _bookMachineBtnR;
                    _bookPageL = _bookMachineBtnL;
                    _isActivePage = true;
                }
                if (_panelBook3.activeInHierarchy && !_isActivePage)
                {
                    _bookPageR = _bookSolutionBtnR;
                    _bookPageL = _bookSolutionBtnL;
                    _isActivePage = true;
                }
            } //ChangePagePanel

            if (ctxValue < -0.5f)
            {
                foreach (var btn in _bookPageL)
                {
                    if (btn.gameObject.activeInHierarchy)
                    {
                        btn.onClick.Invoke();
                        break;
                    }
                }
            }
            else if (ctxValue > 0.5f)
            {
                foreach (var btn in _bookPageR)
                {
                    if (btn.gameObject.activeInHierarchy)
                    {
                        btn.onClick.Invoke();
                        break;
                    }
                }
            }
        }
    }
    public void ReturnFromUI(InputAction.CallbackContext context)
    {
        if (_pauseCanva.activeInHierarchy || _bookUI.activeInHierarchy)
        {
            _pauseCanva.SetActive(false);
            _bookUI.SetActive(false);
            UnpauseTrigger();
        }
    }
}
