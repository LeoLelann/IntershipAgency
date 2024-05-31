using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesMovementAuto : MonoBehaviour//UNUSED
{
    [SerializeField,Range(0.1f,10.0f)] private float _speed;
    [SerializeField,Range(1,10)] private int _dmg=3;
    private PlayerShooter[] _playerPos;
    private Vector3 _dir;
    private float _distance;
    private void Start()
    {
        _dir = new Vector3(0, 0, 0);
        _playerPos = FindObjectsOfType<PlayerShooter>();
        _distance = -1;
    }
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        foreach(PlayerShooter player in _playerPos)
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<PlayerShooter>())
        {
            collision.transform.GetComponent<PlayerShooter>().TakeDmg(_dmg);
        }
    }
}
