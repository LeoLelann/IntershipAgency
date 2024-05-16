using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesMovement : MonoBehaviour
{
    [SerializeField,Range(0.1f,10.0f)] private float _speed;
    [SerializeField] private Vector3 _dir;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        transform.position += _dir * _speed*Time.deltaTime;
    }   
}
