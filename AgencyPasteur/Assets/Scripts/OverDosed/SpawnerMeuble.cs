using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnerMeuble : Interactable
{
    public UnityEvent OnTakeGlassware;
    public UnityEvent OnTakeFrom;
    public UnityEvent OnSnapGlassware;

    [SerializeField] GameObject _ressource;
    [SerializeField] int _limit;
    private int _instantiated = 0;

    public override void Interacted(GameObject player)
    {
        if (player.GetComponentInChildren<Glassware>()==null)
        {
            if (transform.GetComponent<Glassware>()!=null)
            {
                transform.GetComponentInChildren<Glassware>().Interacted(player);
                OnTakeFrom?.Invoke();
            }
            else
            {
                OnTakeGlassware?.Invoke();
                GameObject glassware = Instantiate(_ressource, transform.position, transform.rotation);
                glassware.GetComponent<Glassware>().Interacted(player);
                _instantiated++;
            }
        }
        else if (player.transform.GetComponentInChildren<Glassware>() != null && transform.GetComponentInChildren<Glassware>() == null)
        {
            OnSnapGlassware?.Invoke();
            player.GetComponentInChildren<Glassware>().transform.parent = transform;
            transform.GetComponentInChildren<Glassware>().transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
            transform.GetComponentInChildren<Glassware>().transform.rotation = new Quaternion(-90, 0, 0, 0);
            transform.GetComponentInChildren<Glassware>().transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Glassware>()!=null && collision.transform.parent == null && transform.GetComponentInChildren<Glassware>() == null)
        {
            OnSnapGlassware?.Invoke();
            collision.transform.parent = transform;
            collision.transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
            collision.transform.rotation = new Quaternion(-90, 0, 0, 0);
            collision.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
