using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MixingResult : Interactable
{
    [SerializeField]private UnityEvent _onTakeFrom;
    [SerializeField] private UnityEvent _onSnapGlassware;
    [SerializeField] private UnityEvent _onMixSuccess;
    [SerializeField] private UnityEvent _onMixBad;


    [SerializeField] private Paillaisse _ingr1;
    [SerializeField] private Paillaisse _ingr2;
    private Glassware _in1;
    private Glassware _in2;
    private Glassware _out;
    [SerializeField]private SCMix _mix;
    private void Start()
    {
        Debug.Log(_mix.Mixed.Count);
    }
    public override void Interacted(GameObject player)
    {
        Glassware playerGlassware = player.GetComponentInChildren<Glassware>();
        Glassware currentGlassware = GetComponentInChildren<Glassware>();
        Glassware in1Glassware = _ingr1.GetComponentInChildren<Glassware>();
        Glassware in2Glassware = _ingr2.GetComponentInChildren<Glassware>();
        Debug.Log(currentGlassware);
        Debug.Log(playerGlassware);
        Debug.Log(in1Glassware);
        Debug.Log(in2Glassware);
         if (playerGlassware != null && currentGlassware == null)
        {
            _onSnapGlassware?.Invoke();
            playerGlassware.transform.parent = transform;
            currentGlassware = playerGlassware;
            currentGlassware.transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
            currentGlassware.transform.rotation = new Quaternion(0, 0, 0, 0);
            currentGlassware.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        }
        else
        {
           _out = GetComponentInChildren<Glassware>();
            if ((currentGlassware != null && playerGlassware == null && _out.GlasswareSt != Glassware.glasswareState.EMPTY) || (in1Glassware == null) || (in2Glassware == null))
            {
                _onTakeFrom?.Invoke();
                transform.GetComponentInChildren<Glassware>().Interacted(player);
            }
            else
            {
                if ( in1Glassware!= null && in2Glassware != null)
                {
                    _in1 = in1Glassware;
                    _in2 = in2Glassware;
                    if (_in1.GlasswareSt == Glassware.glasswareState.EMPTY || _in2.GlasswareSt == Glassware.glasswareState.EMPTY)
                    {
                        _onTakeFrom?.Invoke();
                        transform.GetComponentInChildren<Glassware>().Interacted(player);
                    }
                    else
                    {
                        _out.SetGlasswareState(_mix.Mixed.Find(t => t.State[0] == _in1.GlasswareSt && t.State[1] == _in2.GlasswareSt).State[2]);
                        _in1.SetGlasswareState(Glassware.glasswareState.EMPTY);
                        _in2.SetGlasswareState(Glassware.glasswareState.EMPTY);

                        if (_out.GlasswareSt == Glassware.glasswareState.TRASH)
                        {
                            _onMixBad?.Invoke();
                        }
                        else
                        {
                            _onMixSuccess?.Invoke();
                        }
                    }
                }
            }
        }  
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.CompareTag("Glassware") && collision.transform.parent == null && GetComponentInChildren<Glassware>() == null)
        {
            _onSnapGlassware?.Invoke();
            collision.transform.parent = transform;
            collision.transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
            collision.transform.rotation = new Quaternion(-90, 0, 0, 0);
            collision.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
