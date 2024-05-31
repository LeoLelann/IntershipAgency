using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]private float _size;
    [SerializeField]private float _speed;
    [SerializeField]private float _range;
    [SerializeField]private int _damage;
    [SerializeField]private effect _effect;
    private float timing;

    public float Speed { get => _speed; set => _speed = value; }

    private enum effect
    {
        NONE
    };

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = transform.localScale*_size;
    }

    // Update is called once per frame
    void Update()
    {
        timing += Time.deltaTime;
        transform.position += transform.forward * _speed * Time.deltaTime;
        if (timing * _speed > _range)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            other.GetComponent<Enemy>().TakeDamage(_damage);
            Destroy(gameObject);
        }
        else if (other.GetComponent<Cell>() != null)
        {
            if (other.GetComponent<Cell>().Infected)
                other.GetComponent<Cell>().TakeDmg(_damage);
            Destroy(gameObject);
        }
    }
}
