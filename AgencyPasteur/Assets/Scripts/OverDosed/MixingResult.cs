using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class MixingResult : Interactable
{
    [SerializeField]private UnityEvent _onTakeFrom;
    [SerializeField] private UnityEvent _onSnapGlassware;
    [SerializeField] private UnityEvent _onMixSuccess;
    [SerializeField] private UnityEvent _onMixBad;
    [SerializeField] private UnityEvent _onMissingComponentCenter;
    [SerializeField] private UnityEvent _onMissingComponentLeft;
    [SerializeField] private UnityEvent _onMissingComponentRight;
    [SerializeField] private UnityEvent _onReadyToMix;
    [SerializeField] private UnityEvent _onMixing;


    [SerializeField] private Paillaisse _ingr1;
    [SerializeField] private Paillaisse _ingr2;
    [SerializeField] private float _mixDuration;
    [SerializeField] private TutoManager _tuto;

    private Glassware _in1;
    private Glassware _in2;
    private Glassware _out;
    [SerializeField]private SCMix _mix;
    public override void Interacted(GameObject player)
    {
        Glassware playerGlassware = player.GetComponentInChildren<Glassware>();
        Glassware currentGlassware = GetComponentInChildren<Glassware>();
        Glassware in1Glassware = _ingr1.GetComponentInChildren<Glassware>();
        Glassware in2Glassware = _ingr2.GetComponentInChildren<Glassware>();
        Debug.Log(currentGlassware);
        Debug.Log(playerGlassware);
        Debug.Log(in1Glassware);
        Debug.Log(in2Glassware);
        if (playerGlassware != null && currentGlassware == null)
        {
            _onSnapGlassware?.Invoke();
            playerGlassware.transform.parent = transform;
            currentGlassware = playerGlassware;
            currentGlassware.transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
            currentGlassware.transform.rotation = Quaternion.Euler(270, 0, 0);
            currentGlassware.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            currentGlassware.transform.GetComponent<Collider>().enabled = false;
            MixReady();
        }
        else
        {
            _out = GetComponentInChildren<Glassware>();
            if ((currentGlassware != null && playerGlassware == null && _out.GlasswareSt != Glassware.glasswareState.EMPTY) || (in1Glassware == null) || (in2Glassware == null))
            {
                _onTakeFrom?.Invoke();
                if(SceneManager.GetActiveScene().name=="Tutoriel 1")
                {
                    if (_out.GlasswareSt == Glassware.glasswareState.HEATED_ACID_STARCH_DILUTED)
                    {
                        _tuto.Mixed(player);
                    }
                }
                transform.GetComponentInChildren<Glassware>().Interacted(player);
            }
            else
            {
                if (in1Glassware != null && in2Glassware != null)
                {
                    _in1 = in1Glassware;
                    _in2 = in2Glassware;
                    if (_in1.GlasswareSt == Glassware.glasswareState.EMPTY || _in2.GlasswareSt == Glassware.glasswareState.EMPTY)
                    {
                        _onTakeFrom?.Invoke();
                        transform.GetComponentInChildren<Glassware>().Interacted(player);
                    }
                    else
                    {
                        StartCoroutine(Mixing());
                    }
                }
            }
        }
    }
    public void MixReady()
    {
        _in1= _ingr1.GetComponentInChildren<Glassware>();
        _in2= _ingr2.GetComponentInChildren<Glassware>();
        _out = GetComponentInChildren<Glassware>();
        if (_in1 != null && _in2 != null && _out!= null)
        {
            if (_in1.GlasswareSt != Glassware.glasswareState.EMPTY && _in2.GlasswareSt != Glassware.glasswareState.EMPTY && _out.GlasswareSt == Glassware.glasswareState.EMPTY)
            {
                _onReadyToMix.Invoke();
            }
        }
        if (_in1 == null)
        _onMissingComponentLeft.Invoke();
        if (_in2 == null)

        _onMissingComponentRight.Invoke();
        if (_out == null)
        _onMissingComponentCenter.Invoke();
    }

    IEnumerator Mixing()
    {
        _onMixing.Invoke();
        yield return new WaitForSeconds(_mixDuration);
        _out.SetGlasswareState(_mix.Mixed.Find(t => t.State[0] == _in1.GlasswareSt && t.State[1] == _in2.GlasswareSt).State[2]);
        _in1.SetGlasswareState(Glassware.glasswareState.EMPTY);
        _in2.SetGlasswareState(Glassware.glasswareState.EMPTY);

        if (_out.GlasswareSt == Glassware.glasswareState.TRASH)
        {
            _onMixBad?.Invoke();
        }
        else
        {
            _onMixSuccess?.Invoke();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.CompareTag("Glassware") && collision.transform.parent == null && GetComponentInChildren<Glassware>() == null)
        {
            _onSnapGlassware?.Invoke();
            collision.transform.parent = transform;
            collision.transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
            collision.transform.rotation = Quaternion.Euler(270, 0, 0);
            collision.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            collision.transform.GetComponent<Collider>().enabled = false;
            MixReady();
        }
    }
}
