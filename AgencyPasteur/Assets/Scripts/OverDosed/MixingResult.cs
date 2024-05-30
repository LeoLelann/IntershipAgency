using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MixingResult : Interactable
{
    public UnityEvent OnTakeFrom;
    public UnityEvent OnSnapGlassware;
    public UnityEvent OnMixSuccess;
    public UnityEvent OnMixBad;


    [SerializeField] private Paillaisse _ingr1;
    [SerializeField] private Paillaisse _ingr2;
    private Glassware _in1;
    private Glassware _in2;
    private Glassware _out;
    private SCMix _mix;
    private void Start()
    {
        SCMix path = Resources.Load<SCMix>("ScriptableObject/Mix");
        _mix = path;
        Debug.Log(_mix.Mixed.Count);
    }
    public override void Interacted(GameObject player)
    {
        Glassware playerGlassware = player.GetComponentInChildren<Glassware>();
        Glassware currentGlassware = GetComponentInChildren<Glassware>();
        Glassware in1Glassware = _ingr1.GetComponentInChildren<Glassware>();
        Glassware in2Glassware = _ingr2.GetComponentInChildren<Glassware>();
        Debug.Log(currentGlassware);
         if (playerGlassware != null && currentGlassware == null)
        {
            OnSnapGlassware?.Invoke();
            playerGlassware.transform.parent = transform;
            currentGlassware = playerGlassware;
            currentGlassware.transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
            currentGlassware.transform.rotation = new Quaternion(-90, 0, 0, 0);
            currentGlassware.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        }
        else
        {
           _out = GetComponentInChildren<Glassware>();
            if ((currentGlassware != null && playerGlassware == null && _out.GlasswareSt != Glassware.glasswareState.EMPTY) || (in1Glassware == null) || (in2Glassware == null))
            {
                OnTakeFrom?.Invoke();
                Debug.Log("Pourquoi je suis là?");
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
                        OnTakeFrom?.Invoke();
                        Debug.Log("Pourquoi je suis là2?");
                        transform.GetComponentInChildren<Glassware>().Interacted(player);
                    }
                    else
                    {
                        _out.SetGlasswareState(_mix.Mixed.Find(t => t.State[0] == _in1.GlasswareSt && t.State[1] == _in2.GlasswareSt).State[2]);
                        _in1.SetGlasswareState(Glassware.glasswareState.EMPTY);
                        _in2.SetGlasswareState(Glassware.glasswareState.EMPTY);

                        if (_out.GlasswareSt == Glassware.glasswareState.TRASH)
                        {
                            OnMixBad?.Invoke();
                        }
                        else
                        {
                            OnMixSuccess?.Invoke();
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
            OnSnapGlassware?.Invoke();
            collision.transform.parent = transform;
            collision.transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
            collision.transform.rotation = new Quaternion(-90, 0, 0, 0);
            collision.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
