using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] Wave[] _waves;
    [SerializeField] int _timeBtwWaves;
    private Spawner[] spawners;
    private int _waveNumber;
    // Start is called before the first frame update
    void Start()
    {
        spawners = GetComponentsInChildren<Spawner>();
        _waveNumber = 0;
        StartCoroutine(WaveSystem());

    }

    // Update is called once per frame

 IEnumerator WaveSystem()
    {
        Debug.Log(_waveNumber);
        yield return new WaitForSeconds(2);
        foreach (GameObject gameObject in _waves[_waveNumber].wave)
        {
            int count = UnityEngine.Random.Range(0,spawners.Length);
            spawners[count].Spawn(gameObject);
            yield return new WaitForSeconds(0.02f);
        }
        _waveNumber++;
        yield return new WaitForSeconds(1);
        if (_waveNumber < _waves.Length)
        {
            StartCoroutine(WaveSystem());
        }
    }
}
[Serializable]
public class Wave
{
    [SerializeField] public GameObject[] wave;
}
