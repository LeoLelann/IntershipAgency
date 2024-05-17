using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixingResult : Interactable
{
    [SerializeField] private Paillaisse _ingr1;
    [SerializeField] private Paillaisse _ingr2;
    private Glassware _glassware1;
    private Glassware _glassware2;
    private Glassware _glassware3;
    private void Start()
    {
    }
    public override void Interacted(GameObject player)
    {
        _glassware3 = GetComponentInChildren<Glassware>();
        Debug.Log(_ingr1.transform.GetComponentInChildren<Glassware>() == null);
        Debug.Log(_ingr2.transform.GetComponentInChildren<Glassware>() == null);
        Debug.Log((transform.GetComponentInChildren<Glassware>() != null && player.transform.GetComponentInChildren<Glassware>() == null && _glassware3.GlasswareSt != Glassware.glasswareState.EMPTY));
        if ((transform.GetComponentInChildren<Glassware>()!=null && player.transform.GetComponentInChildren<Glassware>()==null && _glassware3.GlasswareSt != Glassware.glasswareState.EMPTY) ||(_ingr1.transform.GetComponentInChildren<Glassware>()==null)||(_ingr2.transform.GetComponentInChildren<Glassware>()==null))
        {
            transform.GetComponentInChildren<Glassware>().Interacted(player);
        }
        else
        {
            Debug.Log(_ingr1.transform.GetComponentInChildren<Glassware>().GlasswareSt);
            if (_ingr1.transform.GetComponentInChildren<Glassware>() !=null&& _ingr2.GetComponentInChildren<Glassware>()!=null)//version provisoire a terme faire avec un scripatble avec une liste avec tous les mélanges et résultats pour les gd.
            {
                _glassware1 = _ingr1.transform.GetComponentInChildren<Glassware>();
                _glassware2 = _ingr2.transform.GetComponentInChildren<Glassware>();
                if (_glassware1.GlasswareSt == Glassware.glasswareState.EMPTY || _glassware2.GlasswareSt == Glassware.glasswareState.EMPTY)
                {
                    transform.GetComponentInChildren<Glassware>().Interacted(player);
                }
                else
                {
                    Debug.Log(_glassware1.GlasswareSt);
                    if (((_glassware1.GlasswareSt == Glassware.glasswareState.STARCH && _glassware2.GlasswareSt == Glassware.glasswareState.TALC) || (_glassware1.GlasswareSt == Glassware.glasswareState.TALC && _glassware2.GlasswareSt == Glassware.glasswareState.STARCH)) && _glassware3.GlasswareSt == Glassware.glasswareState.EMPTY)
                    {
                        _glassware3.SetGlasswareState(Glassware.glasswareState.THICK_POWDER);
                        _glassware1.SetGlasswareState(Glassware.glasswareState.EMPTY);
                        _glassware2.SetGlasswareState(Glassware.glasswareState.EMPTY);
                    }
                    else if (_glassware1.GlasswareSt != Glassware.glasswareState.EMPTY && _glassware2.GlasswareSt != Glassware.glasswareState.EMPTY && _glassware3.GlasswareSt == Glassware.glasswareState.EMPTY)
                    {
                        _glassware3.SetGlasswareState(Glassware.glasswareState.TRASH);
                        _glassware1.SetGlasswareState(Glassware.glasswareState.EMPTY);
                        _glassware2.SetGlasswareState(Glassware.glasswareState.EMPTY);
                    }
                }               
            }
        }  
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.CompareTag("Glassware") && collision.transform.parent == null && transform.GetComponentInChildren<Glassware>() == null)
        {
            collision.transform.parent = transform;
            collision.transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
            collision.transform.rotation = new Quaternion(-90, 0, 0, 0);
            collision.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
