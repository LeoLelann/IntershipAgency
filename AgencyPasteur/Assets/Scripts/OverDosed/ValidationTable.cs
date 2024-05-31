using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ValidationTable : Interactable
{
    public UnityEvent OnValidate;
    public UnityEvent OnInvalidate;

    [SerializeField]private List<Glassware.glasswareState> ToFound=new List<Glassware.glasswareState>();
    [SerializeField]private List<Glassware.glasswareState> Found=new List<Glassware.glasswareState>();
    private Glassware _glassware;

    private void Start()
    {
        _glassware = GetComponentInChildren<Glassware>();
        ToFound.Add(Glassware.glasswareState.HEATED_ACID_STARCH_DILUTED);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.CompareTag("Glassware") && collision.transform.parent == null && _glassware == null)
        {
            collision.transform.parent = transform;
            collision.transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
            collision.transform.rotation = new Quaternion(-90, 0, 0, 0);
            collision.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            Validation();
        }
    }
    public override void Interacted(GameObject player)
    {

        if (_glassware != null && player.transform.GetComponentInChildren<Glassware>() == null)
        {
            transform.GetComponentInChildren<Glassware>().Interacted(player);
        }
        else if (player.transform.GetComponentInChildren<Glassware>() != null && transform.GetComponentInChildren<Glassware>() == null)
        {
            player.GetComponentInChildren<Glassware>().transform.parent = transform;
            transform.GetComponentInChildren<Glassware>().transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
            transform.GetComponentInChildren<Glassware>().transform.rotation = new Quaternion(-90, 0, 0, 0);
            transform.GetComponentInChildren<Glassware>().transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            Validation();
        }
    }

    public void Validation()
    {
        if (ToFound.Contains(_glassware.GlasswareSt)&&!Found.Contains(_glassware.GlasswareSt))
        {
            OnValidate?.Invoke();
            Found.Add(_glassware.GlasswareSt);
        }
        else
        {
            OnInvalidate?.Invoke();
        }
        Destroy(_glassware.gameObject);
    }

}
