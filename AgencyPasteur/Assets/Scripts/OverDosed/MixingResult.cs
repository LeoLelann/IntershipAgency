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
        StartCoroutine(Test());
    }
    public override void Interacted(GameObject player)
    {
        Debug.Log(_ingr1.transform.GetComponentInChildren<Glassware>().glasswareSt);
        if (_ingr1.transform.childCount > 0 && _ingr2.transform.childCount > 0)//version provisoire a terme faire avec un scripatble avec une liste avec tous les mélanges et résultats pour les gd.
        {
            _glassware1 = _ingr1.transform.GetComponentInChildren<Glassware>();
            _glassware2 = _ingr2.transform.GetComponentInChildren<Glassware>();
            _glassware3 = GetComponentInChildren<Glassware>();
            Debug.Log(_glassware1.glasswareSt);
            if (((_glassware1.glasswareSt == Glassware.glasswareState.STARCH && _glassware2.glasswareSt == Glassware.glasswareState.TALC) || (_glassware1.glasswareSt == Glassware.glasswareState.TALC && _glassware2.glasswareSt == Glassware.glasswareState.STARCH))&&_glassware3.glasswareSt==Glassware.glasswareState.EMPTY)
            {
                _glassware3.SetGlasswareState(Glassware.glasswareState.THICK_POWDER);
                _glassware1.SetGlasswareState(Glassware.glasswareState.EMPTY);
                _glassware2.SetGlasswareState(Glassware.glasswareState.EMPTY);
            }
            else if (_glassware1.glasswareSt != Glassware.glasswareState.EMPTY && _glassware2.glasswareSt != Glassware.glasswareState.EMPTY&&_glassware3.glasswareSt==Glassware.glasswareState.EMPTY)
            {
                _glassware3.SetGlasswareState(Glassware.glasswareState.TRASH);
                _glassware1.SetGlasswareState(Glassware.glasswareState.EMPTY);
                _glassware2.SetGlasswareState(Glassware.glasswareState.EMPTY);
            }
        }
    }
    IEnumerator Test()
    {
        yield return new WaitForSeconds(3);
        Interacted(gameObject);

    }
}
