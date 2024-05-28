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
    private Glassware _glassware1;
    private Glassware _glassware2;
    private Glassware _glassware3;
    private SCMix _mix;
    private void Start()
    {
        SCMix path = Resources.Load<SCMix>("ScriptableObject/Mix");
        _mix = path;
        Debug.Log(_mix.Mixed.Count);
    }
    public override void Interacted(GameObject player)
    {
         if (player.transform.GetComponentInChildren<Glassware>() != null && transform.GetComponentInChildren<Glassware>() == null)
        {
            OnSnapGlassware?.Invoke();
            player.GetComponentInChildren<Glassware>().transform.parent = transform;
            transform.GetComponentInChildren<Glassware>().transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
            transform.GetComponentInChildren<Glassware>().transform.rotation = new Quaternion(-90, 0, 0, 0);
            transform.GetComponentInChildren<Glassware>().transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        }
        else
        {
            _glassware3 = GetComponentInChildren<Glassware>();
            if ((transform.GetComponentInChildren<Glassware>() != null && player.transform.GetComponentInChildren<Glassware>() == null && _glassware3.GlasswareSt != Glassware.glasswareState.EMPTY) || (_ingr1.transform.GetComponentInChildren<Glassware>() == null) || (_ingr2.transform.GetComponentInChildren<Glassware>() == null))
            {
                OnTakeFrom?.Invoke();
                transform.GetComponentInChildren<Glassware>().Interacted(player);
            }
            else
            {
                Debug.Log(_ingr1.transform.GetComponentInChildren<Glassware>().GlasswareSt);
                if (_ingr1.transform.GetComponentInChildren<Glassware>() != null && _ingr2.GetComponentInChildren<Glassware>() != null)
                {
                    _glassware1 = _ingr1.transform.GetComponentInChildren<Glassware>();
                    _glassware2 = _ingr2.transform.GetComponentInChildren<Glassware>();
                    if (_glassware1.GlasswareSt == Glassware.glasswareState.EMPTY || _glassware2.GlasswareSt == Glassware.glasswareState.EMPTY)
                    {
                        OnTakeFrom?.Invoke();
                        transform.GetComponentInChildren<Glassware>().Interacted(player);
                    }
                    else
                    {
                        _glassware3.SetGlasswareState(_mix.Mixed.Find(t => t.State[0] == _glassware1.GlasswareSt && t.State[1] == _glassware2.GlasswareSt).State[2]);
                        _glassware1.SetGlasswareState(Glassware.glasswareState.EMPTY);
                        _glassware2.SetGlasswareState(Glassware.glasswareState.EMPTY);

                        if (_glassware3.GlasswareSt == Glassware.glasswareState.TRASH)
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
        if (collision.rigidbody.CompareTag("Glassware") && collision.transform.parent == null && transform.GetComponentInChildren<Glassware>() == null)
        {
            OnSnapGlassware?.Invoke();
            collision.transform.parent = transform;
            collision.transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
            collision.transform.rotation = new Quaternion(-90, 0, 0, 0);
            collision.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
