using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixingResult : Interactable
{
    [SerializeField] private Paillaisse _ingr1;
    [SerializeField] private Paillaisse _ingr2;
    private Glassware _glassware1;
    private Glassware _glassware2;
    [SerializeField] GameObject _ressource;

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
            Debug.Log(_glassware1.glasswareSt);
            if ((_glassware1.glasswareSt == Glassware.glasswareState.STARCH && _glassware2.glasswareSt == Glassware.glasswareState.TALC) || (_glassware1.glasswareSt == Glassware.glasswareState.TALC && _glassware2.glasswareSt == Glassware.glasswareState.STARCH))
            {
               GameObject result= Instantiate(_ressource, new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), transform.rotation, transform);
                result.GetComponent<Glassware>().SetGlasswareState(Glassware.glasswareState.THICK_POWDER);
            }
            else if (_glassware1.glasswareSt != Glassware.glasswareState.EMPTY && _glassware2.glasswareSt != Glassware.glasswareState.EMPTY)
            {
                GameObject result = Instantiate(_ressource, new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), transform.rotation, transform);
                result.GetComponent<Glassware>().SetGlasswareState(Glassware.glasswareState.TRASH);
            }
        }
    }
    IEnumerator Test()
    {
        yield return new WaitForSeconds(3);
        Interacted(gameObject);

    }
}
