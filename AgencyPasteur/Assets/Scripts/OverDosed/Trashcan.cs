using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trashcan : Interactable
{
    // Start is called before the first frame update
    [SerializeField] private UnityEvent _onThrowAway;
    public override void Interacted(GameObject player)
    {
        player.GetComponent<Player>().Anim.SetBool("IsHolding", true);
        GameObject toBeDestroyed = player.GetComponentInChildren<Glassware>().gameObject;
        if (toBeDestroyed != null)
        {
            _onThrowAway.Invoke();
            Destroy(toBeDestroyed);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.CompareTag("Glassware") && collision.transform.parent == null)
        {
            _onThrowAway.Invoke();
            Destroy(collision.gameObject);
        }
    }
}
