using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMeuble : MonoBehaviour
{
    [SerializeField] GameObject _ressource;
    [SerializeField] int _limit;
    private int _instantiated = 0;

    private void Update()
    {
        if (transform.childCount < 1&&_instantiated<_limit)
        {
            Instantiate(_ressource, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation,transform);
            _instantiated++;
        }
    }
}
