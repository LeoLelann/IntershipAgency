using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesMovementAuto : MonoBehaviour
{
    [SerializeField,Range(0.1f,10.0f)] private float _speed;
    private Mov[] _playerPos;
    private Vector3 _dir;
    private float _distance;
    private void Start()
    {
        _dir = new Vector3(0, 0, 0);
        _playerPos = FindObjectsOfType<Mov>();
        _distance = -1;
    }
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        foreach(Mov player in _playerPos)
        {
            if (_distance == -1 || _distance > Vector3.Distance(player.gameObject.transform.position, transform.position))
            {
                _distance= Vector3.Distance(player.gameObject.transform.position, transform.position);
                _dir = Vector3.Normalize(player.gameObject.transform.position - transform.position);
            }
        }
        _distance = -1;
        transform.position += _dir * _speed*Time.deltaTime;
    }   
}
