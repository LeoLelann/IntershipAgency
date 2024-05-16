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
            collision.transform.position = new Vector3(transform.position.x, transform.position.y+0.3f,transform.position.z);
            collision.transform.rotation = new Quaternion(0,0,0,0);
            
        }
    }
    public override void Interacted(GameObject player)
    {
        if (transform.childCount == 1&& player.transform.childCount == 1)
        {
            transform.GetComponentInChildren<Glassware>().Interacted(player);
        }
    }
}
