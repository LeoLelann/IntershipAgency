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
    private Glassware _glassware;
    [SerializeField] int _limit;
    private int _instantiated = 0;

    public override void Interacted(GameObject player)
    {
        _glassware = GetComponentInChildren<Glassware>();
        Glassware playerGlassware = GetComponentInChildren<Glassware>();
        if (playerGlassware==null)
        {
            if (_glassware!=null)
            {
                _glassware.Interacted(player);
                OnTakeFrom?.Invoke();
            }
            else
            {
                OnTakeGlassware?.Invoke();
                GameObject glassware = Instantiate(_ressource, transform.position, Quaternion.identity);
                glassware.GetComponent<Glassware>().Interacted(player);
                _instantiated++;
            }
        }
        else if (playerGlassware != null && _glassware == null)
        {
            OnSnapGlassware?.Invoke();
            playerGlassware.transform.parent = transform;
            _glassware = GetComponentInChildren<Glassware>();
            _glassware.transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
            _glassware.transform.rotation = Quaternion.Euler(270, 0, 0);

            _glassware.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Glassware>()!=null && collision.transform.parent == null && _glassware == null)
        {
            OnSnapGlassware?.Invoke();
            collision.transform.parent = transform;
            collision.transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
            collision.transform.localRotation = Quaternion.Euler(270, 0, 0);
            collision.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
