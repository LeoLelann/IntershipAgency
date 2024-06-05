using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutoManager : MonoBehaviour
{
    [SerializeField] UnityEvent _onStartTuto;
    [SerializeField] UnityEvent _onGotElement;
    [SerializeField] UnityEvent _onHeated;
    [SerializeField] UnityEvent _onDiluted1Element;
    [SerializeField] UnityEvent _onDiluted2Element;
    [SerializeField] UnityEvent _onMixed;
    [SerializeField] UnityEvent _onSent;

    List<GameObject> _playersDil1 = new List<GameObject>();
    List<GameObject> _playersDil2 = new List<GameObject>();
    List<GameObject> _playersHeat = new List<GameObject>();
    List<GameObject> _playersMix = new List<GameObject>();


    private void Start()
    {
        _onStartTuto.Invoke();
    }
    public void TookElement()
    {
        _onGotElement.Invoke();
    }
    public void Heated(GameObject player)
    {
        if (_playersHeat.Contains(player))
        {
            _playersHeat.Add(player);
            if (_playersHeat.Count == 3)
            {
                _onHeated.Invoke();
                _playersHeat.Add(gameObject);
            }
        }
    }
    public void Diluted1(GameObject player)
    {
        if (_playersDil1.Contains(player))
        {
            _playersDil1.Add(player);
            if (_playersDil1.Count == 3)
            {
                _onDiluted1Element.Invoke();
                _playersDil1.Add(gameObject);
            }
        }
    }
    public void Diluted2(GameObject player)
    {
        if(_playersDil2.Contains(player))
        {
            _playersDil2.Add(player);
            if (_playersDil2.Count == 3)
            {
                _onDiluted2Element.Invoke();
                _playersDil2.Add(gameObject);
            }
        }
    }
    public void Mixed(GameObject player)
    {
        if (_playersMix.Contains(player))
        {
            _playersMix.Add(player);
            if (_playersMix.Count == 3)
            {
                _onMixed.Invoke();
                _playersMix.Add(gameObject);
            }
        }
    }
    public void Sent()
    {
        _onSent.Invoke();
    }
}
