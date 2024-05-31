using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Glassware : Interactable
{
    [SerializeField] private UnityEvent _onThrown;
    [SerializeField] private UnityEvent _onDrop;
    [SerializeField] private UnityEvent _onChangeState;
    [SerializeField] private UnityEvent _onPicked;
    [SerializeField] private UnityEvent _onCollisionWhenThrown;

    public enum glasswareState
    {
        EMPTY,
        WATER,
        ACID,
        TALC,
        STARCH,
        ACID_STARCH,
        ACID_TALC,
        THICK_POWDER,
        ACID_STARCH_DILUTED,
        ACID_TALC_DILUTED,
        THICK_POWDER_DILUTED,
        STARCH_DILUTED,
        TALC_DILUTED,
        ACID_DILUTED,
        HEATED_ACID,
        HEATED_TALC,
        HEATED_STARCH,
        HEATED_ACID_STARCH,
        HEATED_ACID_TALC,
        HEATED_THICK_POWDER,
        HEATED_ACID_STARCH_DILUTED,
        HEATED_ACID_TALC_DILUTED,
        HEATED_THICK_POWDER_DILUTED,
        HEATED_STARCH_DILUTED,
        HEATED_TALC_DILUTED,
        HEATED_ACID_DILUTED,
        DIRTY,
        TRASH
    };
    private float _heat;
    private bool isThrown;
    private Transform _parentTransform;
    private Rigidbody _rgbd;
    [SerializeField] private float _throwPower=2;
    private Collider _collider;
    private MeshRenderer _meshRend;
    [SerializeField]private glasswareState _glasswareSt=glasswareState.EMPTY;

    public glasswareState GlasswareSt { get => _glasswareSt; }

    private void Awake()
    {
        
        _rgbd = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _meshRend = GetComponent<MeshRenderer>();
    }
    private void Start()
    {
        _heat = 0;
        isThrown = false;
        OnStateValueChange(_glasswareSt);
    }

    public void Thrown()
    {
        _onThrown?.Invoke();
        isThrown = true;
        _rgbd.constraints = RigidbodyConstraints.None;
        _rgbd.velocity = new Vector3(transform.parent.transform.forward.x * _throwPower, 0.1f, transform.parent.transform.forward.z * _throwPower);
        transform.parent = null;
        _collider.enabled = true;
    }
    public void Drop()
    {
        _onDrop?.Invoke();
        transform.parent = null;
        _rgbd.constraints = RigidbodyConstraints.None;
        _collider.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isThrown)
        {
            isThrown = false;
            _onCollisionWhenThrown?.Invoke();

        }
    }

    public override void Interacted(GameObject player)
    {
        if (player.transform.GetComponentInChildren<Glassware>()==null&&(transform.parent==null||transform.parent.GetComponent<Player>()==null)) 
        {
            _onPicked?.Invoke();
            transform.rotation = Quaternion.Euler(270,0,0);
            transform.parent = player.transform;
            transform.localPosition = new Vector3(0, 0.5f, 1);
           _rgbd.constraints = RigidbodyConstraints.FreezeAll;
            _parentTransform = GetComponentInParent<Transform>();
            _collider.enabled = false;
        }
    }
    public void SetGlasswareState(glasswareState state)
    {
        _glasswareSt = state;
        if (!GameManager.Instance.Found.Contains(state))
        {
            GameManager.Instance.AddElement(state);
            Debug.Log("w");
        }
        OnStateValueChange(_glasswareSt);
    }
    private void OnStateValueChange(glasswareState state)
    {
        _onChangeState?.Invoke();
         switch (state)
        {
            case (glasswareState.EMPTY):
                _meshRend.material.color = Color.gray;
                break;
            case (glasswareState.ACID):
                _meshRend.material.color = new Color(1,0.9f,0);
                break;
            case (glasswareState.STARCH):
                _meshRend.material.color = new Color(1, 0.75f, 0.8f); ;
                break;
            case (glasswareState.TALC):
                _meshRend.material.color = Color.blue;
                break;
            case (glasswareState.HEATED_ACID):
                _meshRend.material.color =  new Color(1.0f, 0.64f, 0.0f);
                break;
            case (glasswareState.HEATED_STARCH):
                _meshRend.material.color = Color.red;
                break;
            case (glasswareState.HEATED_TALC):
                _meshRend.material.color = new Color(0.5f,0,0.5f); ;
                break;
            case (glasswareState.TRASH):
                _meshRend.material.color = Color.black;
                break;
            case (glasswareState.THICK_POWDER):
                _meshRend.material.color = Color.green;
                break;
            case (glasswareState.ACID_DILUTED):
                _meshRend.material.color = new Color(1,1,0.6f);
                break;
            case (glasswareState.HEATED_ACID_STARCH_DILUTED):
                _meshRend.material.color = new Color(0.5f, 0.25f, 0);
                break;
            case (glasswareState.WATER):
                _meshRend.material.color = new Color(0,0.2f,1);
                break;
        }
    }
}

