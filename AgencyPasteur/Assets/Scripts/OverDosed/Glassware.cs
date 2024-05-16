using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glassware : Interactable
{
    public enum glasswareState
    {
        EMPTY,
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

    public glasswareState glasswareSt=glasswareState.EMPTY;
    private void Awake()
    {
        
        _rgbd = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        _heat = 0;
        isThrown = false;
    }

    public void Thrown()
    {
        transform.parent = null;
        _rgbd.constraints = RigidbodyConstraints.None;
        _rgbd.AddForce(_parentTransform.forward * _throwPower);
    }
    public void Drop()
    {
        transform.parent = null;
        _rgbd.constraints = RigidbodyConstraints.None;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player")&&isThrown)
        {
            transform.parent = collision.transform;
            isThrown = false;

        }
    }

    public override void Interacted(GameObject player)
    {
        if (player.transform.childCount == 1) ;
        {
            transform.localRotation = new Quaternion(0,0,0,0);
            transform.parent = player.transform;
            transform.localPosition = new Vector3(0, 0.5f, 1);
           _rgbd.constraints = RigidbodyConstraints.FreezeAll;
            _parentTransform = GetComponentInParent<Transform>();
        }
    }
    public void SetGlasswareState(glasswareState state)
    {
        glasswareSt = state;
    }
}

