using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : Interactable
{
    // Start is called before the first frame update
    public override void Interacted(GameObject player)
    {
        GameObject toBeDestroyed = player.GetComponentInChildren<Glassware>().gameObject;
        if (toBeDestroyed != null)
        {
            Destroy(toBeDestroyed);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.CompareTag("Glassware") && collision.transform.parent == null)
        {
            Destroy(collision.gameObject);
        }
    }
}
