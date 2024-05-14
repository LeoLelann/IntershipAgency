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
        TALC_STARCH,
        STARCH_PASTE,
        TALC_PASTE,
        TRASH
    };
    private float _heat;
    private bool isThrown;
    private Transform _parentTransform;
    private Rigidbody _rgbd;
    [SerializeField] private float _throwPower=2; 

    private void Start()
    {
        _heat = 0;
        isThrown = false;
        _parentTransform = GetComponentInParent<Transform>();
        _rgbd = GetComponent<Rigidbody>();
    }

    public void Thrown()
    {
        bool isThrown = true;
       Vector3 throwDir= _parentTransform.forward.normalized;
       _parentTransform.DetachChildren();
        _rgbd.AddForce(throwDir * _throwPower);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            transform.parent = collision.transform;
        }
        isThrown = false;
    }

    public override void Interacted(GameObject player)
    {
        if (player.transform.childCount == 0)
        {
            transform.parent = player.transform;
        }
    }
}

