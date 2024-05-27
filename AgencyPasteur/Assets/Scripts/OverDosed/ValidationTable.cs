using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ValidationTable : Interactable
{
    public UnityEvent OnValidate;
    public UnityEvent OnInvalidate;

    [SerializeField]private List<Glassware.glasswareState> Found=new List<Glassware.glasswareState>();

    private void Start()
    {
        Found.Add(Glassware.glasswareState.EMPTY);
        Found.Add(Glassware.glasswareState.ACID);
        Found.Add(Glassware.glasswareState.WATER);
        Found.Add(Glassware.glasswareState.STARCH);
        Found.Add(Glassware.glasswareState.TALC);
        Found.Add(Glassware.glasswareState.TRASH);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.CompareTag("Glassware") && collision.transform.parent == null && transform.GetComponentInChildren<Glassware>() == null)
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
        Debug.Log(transform.GetComponentInChildren<Glassware>() == null && player.transform.GetComponent<Glassware>() != null);
        Debug.Log(transform.GetComponentInChildren<Glassware>() == null);
        Debug.Log(player.transform.GetComponent<Glassware>() != null);
        if (transform.GetComponentInChildren<Glassware>() != null && player.transform.GetComponentInChildren<Glassware>() == null)
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
        if (!Found.Contains(transform.GetComponentInChildren<Glassware>().GlasswareSt))
        {
            OnValidate?.Invoke();
            Found.Add(transform.GetComponentInChildren<Glassware>().GlasswareSt);
        }
        else
        {
            OnInvalidate?.Invoke();
        }
        Destroy(transform.GetComponentInChildren<Glassware>().gameObject);
    }

}
