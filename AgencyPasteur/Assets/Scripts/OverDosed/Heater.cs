using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Heater : Interactable
{
    [SerializeField]private UnityEvent _onStartHeating;
    public UnityEvent _onStopHeating;
    public UnityEvent _onBurning;
    public UnityEvent _onTakeFrom;
    public UnityEvent _onSnapGlassware;

    [SerializeField]SCHeat _heat;
    [SerializeField] private float secondsTillHeated=3;

    private void Start()
    {
      
    }
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.CompareTag("Glassware") && collision.transform.parent == null && transform.GetComponentInChildren<Glassware>() == null)
        {
            _onSnapGlassware?.Invoke();
            collision.transform.parent = transform;
            collision.transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
            Quaternion.Euler(270, 0, 0);
            collision.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            collision.transform.GetComponent<Collider>().enabled = false;
            StartCoroutine(Heating());
        }
    }

    IEnumerator Heating()
    {
        Glassware glassware = GetComponentInChildren<Glassware>();
        if (glassware.GlasswareSt!= Glassware.glasswareState.EMPTY && glassware.GlasswareSt != Glassware.glasswareState.TRASH)
        {
            _onStartHeating?.Invoke();
            if (_heat.Heated.Find(x => x.State[0] == glassware.GlasswareSt).State[1] == Glassware.glasswareState.TRASH)
            {
                _onBurning?.Invoke();
            }
        }
        else
        {
            _onStopHeating?.Invoke();
            StopAllCoroutines();
        }
        Debug.Log("Start");
        yield return new WaitForSeconds(secondsTillHeated);
        if (glassware != null)
        {
            glassware.SetGlasswareState( _heat.Heated.Find(x => x.State[0] == glassware.GlasswareSt).State[1]);
            StartCoroutine(Heating());
        }
        else
        {
            _onStopHeating?.Invoke();
            StopAllCoroutines();
        }
    }
    public override void Interacted(GameObject player)
    {
        Glassware glassware = GetComponentInChildren<Glassware>();
        Glassware glasswarePlayer = player.GetComponentInChildren<Glassware>();
        if (glassware!=null && glasswarePlayer==null)
        {
            _onTakeFrom?.Invoke();
            _onStopHeating?.Invoke();
            glassware.Interacted(player);
            StopAllCoroutines();
        }
        else if (glasswarePlayer != null && glassware == null)
        {
            _onSnapGlassware?.Invoke();
            glasswarePlayer.transform.parent = transform;
            glassware = glasswarePlayer;
            glassware.transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
            Quaternion.Euler(270, 0, 0);
            glassware.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            StartCoroutine(Heating());
        }
    }
}
