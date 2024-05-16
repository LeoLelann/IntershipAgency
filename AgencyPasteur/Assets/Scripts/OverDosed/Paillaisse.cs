using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paillaisse : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.GetComponent<Glassware>() != null);
        if (collision.rigidbody.CompareTag("Glassware") && collision.transform.parent == null && transform.GetComponentInChildren<Glassware>() == null) 
        { 
            collision.transform.parent = transform;
            collision.transform.position = new Vector3(transform.position.x, transform.position.y+0.3f,transform.position.z);
        }
    }
}
