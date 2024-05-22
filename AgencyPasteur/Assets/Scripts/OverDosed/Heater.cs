using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heater : Interactable
{
    SCHeat _heat;
    [SerializeField] private float secondsTillHeated=3;

    private void Start()
    {
        SCHeat path = Resources.Load<SCHeat>("ScriptableObject/Heat");
        _heat = path;
    }
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.CompareTag("Glassware") && collision.transform.parent == null && transform.GetComponentInChildren<Glassware>() == null)
        {
            collision.transform.parent = transform;
            collision.transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
            collision.transform.rotation = new Quaternion(-90, 0, 0, 0);
            collision.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            StartCoroutine(Heating());
        }
    }

    IEnumerator Heating()
    {
        Debug.Log("Start");
        yield return new WaitForSeconds(secondsTillHeated);
        if (transform.GetComponentInChildren<Glassware>() != null)
        {
            transform.GetComponentInChildren<Glassware>().SetGlasswareState( _heat.Heated.Find(x => x.State[0] == transform.transform.GetComponentInChildren<Glassware>().GlasswareSt).State[1]);
            StartCoroutine(Heating());
        }
    }
    public override void Interacted(GameObject player)
    {
        if (transform.GetComponentInChildren<Glassware>()!=null && player.transform.GetComponentInChildren<Glassware>()==null)
        {
            transform.GetComponentInChildren<Glassware>().Interacted(player);
        }
        else if (player.transform.GetComponentInChildren<Glassware>() != null && transform.GetComponentInChildren<Glassware>() == null)
        {
            player.GetComponentInChildren<Glassware>().transform.parent = transform;
            transform.GetComponentInChildren<Glassware>().transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
            transform.GetComponentInChildren<Glassware>().transform.rotation = new Quaternion(-90, 0, 0, 0);
            transform.GetComponentInChildren<Glassware>().transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            StartCoroutine(Heating());
        }
    }
}
