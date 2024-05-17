using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMeuble : Interactable
{
    [SerializeField] GameObject _ressource;
    [SerializeField] int _limit;
    private int _instantiated = 0;

    public override void Interacted(GameObject player)
    {
        Debug.Log("ça passe;");
        if (player.GetComponentInChildren<Glassware>()==null)
        {
            if (transform.GetComponent<Glassware>()!=null)
            {
                transform.GetComponentInChildren<Glassware>().Interacted(player);
            }
            else
            {
                GameObject glassware = Instantiate(_ressource, transform.position, transform.rotation);
                glassware.GetComponent<Glassware>().Interacted(player);
                _instantiated++;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Glassware>()!=null && collision.transform.parent == null && transform.GetComponentInChildren<Glassware>() == null)
        {
            collision.transform.parent = transform;
            collision.transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
            collision.transform.rotation = new Quaternion(-90, 0, 0, 0);
            collision.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
