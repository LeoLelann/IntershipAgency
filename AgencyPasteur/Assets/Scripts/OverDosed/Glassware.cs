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
        DIRTY,
        TRASH
    };
    private float _heat;
    private bool isThrown;
    private Transform _parentTransform;
    private Rigidbody _rgbd;
    [SerializeField] private float _throwPower=2;
    public glasswareState glasswareSt=glasswareState.EMPTY;
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
        transform.parent = null;
        _rgbd.AddForce(throwDir * _throwPower);
        _rgbd.freezeRotation = false;
        _rgbd.useGravity = true;
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
        Debug.Log("glasswareInteracted");
        if (player.transform.childCount == 1)
        {
            transform.rotation = new Quaternion(0,0,0,0);
            transform.parent = player.transform;
            transform.position = new Vector3(player.transform.position.x + 0.3f, player.transform.position.y + 0.3f, player.transform.position.z);
            _rgbd.freezeRotation = true;
            _rgbd.useGravity = false;
            _rgbd.velocity = Vector3.zero;
        }
    }
    public void SetGlasswareState(glasswareState state)
    {
        glasswareSt = state;
    }
}

