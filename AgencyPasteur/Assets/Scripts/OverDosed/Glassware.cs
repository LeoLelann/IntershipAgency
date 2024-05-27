using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Glassware : Interactable
{
    public UnityEvent OnThrown;
    public UnityEvent OnDrop;
    public UnityEvent OnChangeState;
    public UnityEvent OnPicked;
    public UnityEvent OnCollisionWhenThrown;

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

    [SerializeField]private glasswareState _glasswareSt=glasswareState.EMPTY;

    public glasswareState GlasswareSt { get => _glasswareSt; }

    private void Awake()
    {
        
        _rgbd = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        _heat = 0;
        isThrown = false;
        OnStateValueChange(_glasswareSt);
    }

    public void Thrown()
    {
        OnThrown?.Invoke();
        isThrown = true;
        _rgbd.constraints = RigidbodyConstraints.None;
        _rgbd.velocity = new Vector3(transform.parent.transform.forward.x * _throwPower, 0.1f, transform.parent.transform.forward.z * _throwPower);
        transform.parent = null;                                         
    }
    public void Drop()
    {
        OnDrop?.Invoke();
        transform.parent = null;
        _rgbd.constraints = RigidbodyConstraints.None;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isThrown)
        {
            isThrown = false;
            OnCollisionWhenThrown?.Invoke();

        }
    }

    public override void Interacted(GameObject player)
    {
        if (player.transform.GetComponentInChildren<Glassware>()==null&&(transform.parent==null||transform.parent.GetComponent<Player>()==null)) 
        {
            OnPicked?.Invoke();
            transform.localRotation = new Quaternion(-90,0,0,0);
            transform.parent = player.transform;
            transform.localPosition = new Vector3(0, 0.5f, 1);
           _rgbd.constraints = RigidbodyConstraints.FreezeAll;
            _parentTransform = GetComponentInParent<Transform>();
        }
    }
    public void SetGlasswareState(glasswareState state)
    {
        _glasswareSt = state;
        OnStateValueChange(_glasswareSt);
    }
    private void OnStateValueChange(glasswareState state)
    {
        OnChangeState?.Invoke();
         switch (state)
        {
            case (glasswareState.EMPTY):
                GetComponent<MeshRenderer>().material.color = Color.gray;
                break;
            case (glasswareState.ACID):
                GetComponent<MeshRenderer>().material.color = new Color(1,0.9f,0);
                break;
            case (glasswareState.STARCH):
                GetComponent<MeshRenderer>().material.color = Color.white;
                break;
            case (glasswareState.TALC):
                GetComponent<MeshRenderer>().material.color = Color.blue;
                break;
            case (glasswareState.HEATED_ACID):
                GetComponent<MeshRenderer>().material.color =  new Color(1.0f, 0.64f, 0.0f);
                break;
            case (glasswareState.HEATED_STARCH):
                GetComponent<MeshRenderer>().material.color = Color.red;
                break;
            case (glasswareState.HEATED_TALC):
                GetComponent<MeshRenderer>().material.color = new Color(0.5f,0,0.5f); ;
                break;
            case (glasswareState.TRASH):
                GetComponent<MeshRenderer>().material.color = Color.black;
                break;
            case (glasswareState.THICK_POWDER):
                GetComponent<MeshRenderer>().material.color = Color.green;
                break;
            case (glasswareState.ACID_DILUTED):
                GetComponent<MeshRenderer>().material.color = new Color(1,1,0.6f);
                break;
            case (glasswareState.HEATED_ACID_TALC_DILUTED):
                GetComponent<MeshRenderer>().material.color = new Color(0.5f, 0.25f, 0);
                break;
        }
    }
}

