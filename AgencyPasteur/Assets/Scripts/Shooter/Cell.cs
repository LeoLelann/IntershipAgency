using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private bool _playersIn;
    [SerializeField] private bool _spawning;
    [SerializeField] private bool _infected;
    [SerializeField] private float _spawnCD=10;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private List<Cell> _neighbours;
    [SerializeField] private float _infectedHP=100;
    [SerializeField]private float _currentHP;

    public float InfectedHP { get => _infectedHP; set => _infectedHP = value; }
    public bool PlayersIn { get => _playersIn; set => _playersIn = value; }
    public bool Infected { get => _infected; set => _infected = value; }

    // Start is called before the first frame update
    void Start()
    {
        _currentHP = _infectedHP;
        foreach(Cell cell in FindObjectsOfType<Cell>())
        {
            if ((Mathf.Abs(transform.position.z-cell.transform.position.z) <= 20&&transform.position.x==cell.transform.position.x)|| (Mathf.Abs(transform.position.x - cell.transform.position.x) <= 40&&transform.position.z==cell.transform.position.z ))
            {
                _neighbours.Add(cell);
            }
        }
    }
    public void TakeDmg(int dmg)
    {
        _currentHP -= dmg;
        if (_currentHP <= 0)
        {
            _infected = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!_spawning && _infected && _neighbours.Find(x => x._playersIn==true)!=null)
        {
            _spawning = true;
            StartCoroutine(SpawnSequence());
        }
        if (_spawning && _neighbours.Find(x => x._playersIn == true) == null)
        {
            _spawning = false;
            StopAllCoroutines();
        }
    }
    IEnumerator SpawnSequence()
    {
        float randx = Random.Range(-3, 3);
        float randz = Random.Range(-3, 3);
        Instantiate(_enemy,new Vector3(transform.position.x+randx,transform.position.y,transform.position.z+randz),transform.rotation);
        yield return new WaitForSeconds(_spawnCD-3*_spawnCD/4*((_infectedHP- _currentHP)/_infectedHP));
        if (_currentHP > 0)
        {
            StartCoroutine(SpawnSequence());
        }
        else
        {
            _infected = false;
        }
    }
}
