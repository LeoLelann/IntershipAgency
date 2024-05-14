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
        if (player.transform.childCount == 0)
        {
            Instantiate(_ressource, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation, player.transform);
            _instantiated++;
        }
    }

}
