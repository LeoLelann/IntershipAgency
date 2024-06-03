using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigerObject : MonoBehaviour
{
    Player _player;

    public void Awake()
    {
        _player = transform.parent.GetComponent<Player>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent("Interactable"))
        {
            _player.isInRange = true;
            _player.range = other.transform.GetComponent<Interactable>();
            _player.range.OnStartShowInteract?.Invoke();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent("Interactable"))
        {
            Glassware glassware = _player.GetComponentInChildren<Glassware>();
            _player.isInRange = true;
            _player.range = other.transform.GetComponent<Interactable>();
            _player.range.OnShowInteract?.Invoke();
            if (glassware == null)
            {
                _player.range.OnShowInterractButMissingGlassware.Invoke();
            }
            else if (glassware.GlasswareSt == Glassware.glasswareState.EMPTY || glassware.GlasswareSt == Glassware.glasswareState.TRASH)
            {
                _player.range.OnShowInterractButMissingComponent.Invoke();
            }
            else
            {
                _player.range.OnShowInterractButHandsFull.Invoke();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent("Interactable"))
        {
            other.transform.GetComponent<Interactable>().OnDontShowInteract?.Invoke();
            _player.isInRange = false;
            _player.range = null;
        }
    }
}
