using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Paillaisse : Interactable
{
    [SerializeField]private UnityEvent OnTakeFrom;
    [SerializeField] private UnityEvent OnSnapGlassware;
    private Glassware _glassware;
    private void Start()
    {
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.CompareTag("Glassware") && collision.transform.parent == null && _glassware == null) 
        {
            OnSnapGlassware?.Invoke();
            collision.transform.parent = transform;
            collision.transform.position = new Vector3(transform.position.x, transform.position.y+1.3f,transform.position.z);
            collision.transform.rotation = Quaternion.Euler(270, 0, 0);
            collision.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
    public override void Interacted(GameObject player)
    {
        _glassware = GetComponentInChildren<Glassware>();
        Glassware playerGlassware = player.GetComponentInChildren<Glassware>();
        if (_glassware!=null&& playerGlassware==null)
        {
            OnTakeFrom?.Invoke();
            transform.GetComponentInChildren<Glassware>().Interacted(player);
        }
        else if (playerGlassware != null && _glassware == null)
        {
            OnSnapGlassware?.Invoke();
            playerGlassware.transform.parent = transform;
            _glassware = playerGlassware;
            _glassware.transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
            _glassware.transform.rotation = Quaternion.Euler(270, 0, 0);
            _glassware.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        }
    }
}
