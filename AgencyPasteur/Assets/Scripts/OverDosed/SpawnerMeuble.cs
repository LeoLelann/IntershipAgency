using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnerMeuble : Interactable
{
    [SerializeField] UnityEvent _onTakeGlassware;
    [SerializeField] UnityEvent _onTakeFrom;
    [SerializeField] UnityEvent _onSnapGlassware;

    [SerializeField] GameObject _ressource;
    private Glassware _glassware;
    [SerializeField] int _limit;
    private int _instantiated = 0;

    public override void Interacted(GameObject player)
    {
        _glassware = GetComponentInChildren<Glassware>();
        Glassware playerGlassware = player.GetComponentInChildren<Glassware>();
        if (playerGlassware == null)
        {
            
                _onTakeGlassware?.Invoke();
                GameObject glassware = Instantiate(_ressource, transform.position, Quaternion.identity);
                glassware.GetComponent<Glassware>().Interacted(player);
                _instantiated++;       
        }
    }
}
