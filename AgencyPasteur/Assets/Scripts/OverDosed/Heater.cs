using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heater : Interactable
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.CompareTag("Glassware") && collision.transform.parent == null && transform.GetComponentInChildren<Glassware>() == null)
        {
            collision.transform.parent = transform;
            collision.transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
            collision.transform.rotation = new Quaternion(0, 0, 0, 0);
            if (collision.transform.GetComponent<Glassware>().glasswareSt != Glassware.glasswareState.EMPTY)
                StartCoroutine(Heating());
        }
    }

    IEnumerator Heating()
    {
        Debug.Log("feur");
        yield return new WaitForSeconds(8);
        Glassware.glasswareState glass = transform.GetComponentInChildren<Glassware>().glasswareSt;
        switch (glass)
        {
            case (Glassware.glasswareState.ACID):
                transform.GetComponentInChildren<Glassware>().glasswareSt = Glassware.glasswareState.HEATED_ACID;
                break; 
            case (Glassware.glasswareState.STARCH):
                transform.GetComponentInChildren<Glassware>().glasswareSt = Glassware.glasswareState.HEATED_STARCH;
                break;
            case (Glassware.glasswareState.TALC):
                transform.GetComponentInChildren<Glassware>().glasswareSt = Glassware.glasswareState.HEATED_TALC;
                break;
            default:
                transform.GetComponentInChildren<Glassware>().glasswareSt = Glassware.glasswareState.TRASH;
                break;
        }
        yield return new WaitForSeconds(8);
        transform.GetComponentInChildren<Glassware>().glasswareSt = Glassware.glasswareState.TRASH;
    }
    public override void Interacted(GameObject player)
    {
        if (transform.childCount == 1 && player.transform.childCount == 1)
        {
            transform.GetComponentInChildren<Glassware>().Interacted(player);
        }
    }
}
