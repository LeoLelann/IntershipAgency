using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paillaisse : Interactable
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.CompareTag("Glassware") && collision.transform.parent == null && transform.GetComponentInChildren<Glassware>() == null) 
        { 
            collision.transform.parent = transform;
            collision.transform.position = new Vector3(transform.position.x, transform.position.y+1.3f,transform.position.z);
            collision.transform.rotation = new Quaternion(-90,0,0,0);
            collision.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
    public override void Interacted(GameObject player)
    {
        if (transform.GetComponentInChildren<Glassware>()!=null&& player.transform.GetComponentInChildren<Glassware>()==null)
        {
            transform.GetComponentInChildren<Glassware>().Interacted(player);
        }
        else if (player.transform.GetComponent<Glassware>() != null && transform.GetComponentInChildren<Glassware>() == null)
        {
            transform.GetComponentInChildren<Glassware>().Interacted(gameObject);
        }
    }
}
