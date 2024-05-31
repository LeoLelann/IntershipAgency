using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnerElement : Interactable
{
    [SerializeField] private UnityEvent _onNoGlasswareToTakeElement;
    [SerializeField] private UnityEvent _onElementTaken; 
    [SerializeField]private UnityEvent _onTakeFrom;
    [SerializeField]private UnityEvent _onSnapGlassware;

    [SerializeField] private MeshRenderer _label;
    private Glassware _glassware;
    public enum Elements
    {
        TALC,
        ACID,
        STARCH
    };
    [SerializeField] private Elements element;

    private void Start()
    {
        _glassware = GetComponentInChildren<Glassware>();
        OnStateValueChange(element);
    }

    public override void Interacted(GameObject player)
    {
        Glassware playerGlassware = player.GetComponentInChildren<Glassware>();
        if (_glassware!=null && playerGlassware==null)
        {
            _onTakeFrom?.Invoke();
            _glassware.Interacted(player);
        }
        else
        {
            if (playerGlassware!=null && playerGlassware.GlasswareSt == Glassware.glasswareState.EMPTY)
            {
                switch (element)
                {
                    case Elements.TALC:
                        playerGlassware.SetGlasswareState(Glassware.glasswareState.TALC);
                        break;
                    case Elements.ACID:
                        playerGlassware.SetGlasswareState(Glassware.glasswareState.ACID);
                        break;
                    case Elements.STARCH:
                        playerGlassware.SetGlasswareState(Glassware.glasswareState.STARCH);
                        break;
                }
                _onElementTaken?.Invoke();
            }
            else
            {
                _onNoGlasswareToTakeElement?.Invoke();
            }
        }
        
    }
    private void OnStateValueChange(Elements state)
    {
        switch (state)
        {
            case (Elements.ACID):                
                    _label.material.color=Color.yellow;       
                break;
            case (Elements.STARCH):
                
                    _label.material.color = new Color(1,0.75f,0.8f);
                
                break;
            case (Elements.TALC):
                
                    _label.material.color = Color.blue;
                
                break;
         }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.CompareTag("Glassware") && collision.transform.parent == null && _glassware == null)
        {
            _onSnapGlassware?.Invoke();
            collision.transform.parent = transform;
            collision.transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
            collision.transform.rotation = Quaternion.Euler(270, 0, 0);
            collision.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        }
    }
}
