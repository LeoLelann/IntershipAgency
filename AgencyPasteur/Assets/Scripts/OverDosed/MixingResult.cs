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
        if ((transform.childCount == 2 && player.transform.childCount == 1 && _glassware3.glasswareSt != Glassware.glasswareState.EMPTY) ||(_ingr1.transform.childCount==0)||(_ingr2.transform.childCount==0))
        {
            transform.GetComponentInChildren<Glassware>().Interacted(player);
        }
        else
        {
            Debug.Log(_ingr1.transform.GetComponentInChildren<Glassware>().glasswareSt);
            if (_ingr1.transform.childCount > 0 && _ingr2.transform.childCount > 0)//version provisoire a terme faire avec un scripatble avec une liste avec tous les mélanges et résultats pour les gd.
            {
                _glassware1 = _ingr1.transform.GetComponentInChildren<Glassware>();
                _glassware2 = _ingr2.transform.GetComponentInChildren<Glassware>();
                if (_glassware1.glasswareSt == Glassware.glasswareState.EMPTY || _glassware2.glasswareSt == Glassware.glasswareState.EMPTY)
                {
                    transform.GetComponentInChildren<Glassware>().Interacted(player);
                }
                else
                {
                    Debug.Log(_glassware1.glasswareSt);
                    if (((_glassware1.glasswareSt == Glassware.glasswareState.STARCH && _glassware2.glasswareSt == Glassware.glasswareState.TALC) || (_glassware1.glasswareSt == Glassware.glasswareState.TALC && _glassware2.glasswareSt == Glassware.glasswareState.STARCH)) && _glassware3.glasswareSt == Glassware.glasswareState.EMPTY)
                    {
                        _glassware3.SetGlasswareState(Glassware.glasswareState.THICK_POWDER);
                        _glassware1.SetGlasswareState(Glassware.glasswareState.EMPTY);
                        _glassware2.SetGlasswareState(Glassware.glasswareState.EMPTY);
                    }
                    else if (_glassware1.glasswareSt != Glassware.glasswareState.EMPTY && _glassware2.glasswareSt != Glassware.glasswareState.EMPTY && _glassware3.glasswareSt == Glassware.glasswareState.EMPTY)
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
            collision.transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
            collision.transform.rotation = new Quaternion(0, 0, 0, 0);

        }
    }
}
