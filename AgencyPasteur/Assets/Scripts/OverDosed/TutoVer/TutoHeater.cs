using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TutoHeater : Interactable
{
    [SerializeField]private UnityEvent _onStartHeating;
    [SerializeField]private UnityEvent _onStopHeating;
    [SerializeField]private UnityEvent _onBurning;
    [SerializeField]private UnityEvent _onTakeFrom;
    [SerializeField]private UnityEvent _onSnapGlassware;
    [SerializeField]private UnityEvent _onCantCook;
    [SerializeField]private UnityEvent _onBurnt;

    [SerializeField]SCHeat _heat;
    [SerializeField] private float secondsTillHeated=3;
    [SerializeField] TutoManager _tuto;

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
        if ( glassware.GlasswareSt != Glassware.glasswareState.TRASH)
        {
            if (_heat.Heated.Find(x => x.State[0] == glassware.GlasswareSt) != null)
            {
                _onStartHeating?.Invoke();
                if (_heat.Heated.Find(x => x.State[0] == glassware.GlasswareSt).State[1] == Glassware.glasswareState.TRASH)
                {
                    _onBurning?.Invoke();
                }
            }
            else
            {
                _onCantCook.Invoke();
                StopAllCoroutines();
            }
        }
        else
        {
            _onStopHeating?.Invoke();
            StopAllCoroutines();
        }
        yield return new WaitForSeconds(secondsTillHeated);
        if (glassware != null)
        {
            if (_heat.Heated.Find(x => x.State[0] == glassware.GlasswareSt) != null)
            {
                glassware.SetGlasswareState(_heat.Heated.Find(x => x.State[0] == glassware.GlasswareSt).State[1]);
                if (glassware.GlasswareSt == Glassware.glasswareState.TRASH)
                    _onBurnt.Invoke();
                StartCoroutine(Heating());
            }
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
            if (glassware.GlasswareSt == Glassware.glasswareState.HEATED_STARCH)
            {
                _tuto.Heated(player);
            }
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
