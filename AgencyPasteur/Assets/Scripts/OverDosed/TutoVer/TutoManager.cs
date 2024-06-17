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
    [SerializeField] ValidateTuto _valid;


   [SerializeField] List<GameObject> _playersDil1 = new List<GameObject>();
   [SerializeField] List<GameObject> _playersDil2 = new List<GameObject>();
   [SerializeField] List<GameObject> _playersHeat = new List<GameObject>();
   [SerializeField] List<GameObject> _playersMix = new List<GameObject>();


    private void Start()
    {
        _onStartTuto.Invoke();
    }
    public void TookElement()
    {
        _onGotElement.Invoke();
        _valid.Reset();
    }
    public void Heated(GameObject player)
    {                

        if (!_playersHeat.Contains(player))
        {
            switch (player.GetComponentInChildren<Animator>().gameObject.name)
            {
                case "Ch_Character_Cat":
                    _valid.CatGood();
                    break;
                case "Ch_Character_Dog_No_EarsRig":
                    _valid.DogGood();
                    break;
                case "Ch_Character_Monkey":
                    _valid.MonkeyGood();
                    break;
            }
            _playersHeat.Add(player);
            switch (player.GetComponentInChildren<Animator>().gameObject.name)
            {
                case "Ch_Character_Cat":
                    _valid.CatGood();
                    break;
                case "Ch_Character_Dog_No_EarsRig":
                    _valid.DogGood();
                    break;
                case "Ch_Character_Monkey":
                    _valid.MonkeyGood();
                    break;
            }
            if (_playersHeat.Count == 3)
            {                

                _onHeated.Invoke();
                _playersHeat.Add(gameObject);
                _valid.Reset();
            }
        }
    }
    public void Diluted1(GameObject player)
    {
        if (!_playersDil1.Contains(player))
        {

            _playersDil1.Add(player);
            switch (player.GetComponentInChildren<Animator>().gameObject.name)
            {
                case "Ch_Character_Cat":
                    _valid.CatGood();
                    break;
                case "Ch_Character_Dog_No_EarsRig":
                    _valid.DogGood();
                    break;
                case "Ch_Character_Monkey":
                    _valid.MonkeyGood();
                    break;
            }
            if (_playersDil1.Count == 3)
            {
                _onDiluted1Element.Invoke();
                _playersDil1.Add(gameObject);
                _valid.Reset();
            }
        }
    }
    public void Diluted2(GameObject player)
    {
        if(!_playersDil2.Contains(player))
        {
            _playersDil2.Add(player);
            switch (player.GetComponentInChildren<Animator>().gameObject.name)
            {
                case "Ch_Character_Cat":
                    _valid.CatGood();
                    break;
                case "Ch_Character_Dog_No_EarsRig":
                    _valid.DogGood();
                    break;
                case "Ch_Character_Monkey":
                    _valid.MonkeyGood();
                    break;
            }
            if (_playersDil2.Count == 3)
            {
                _onDiluted2Element.Invoke();
                _playersDil2.Add(gameObject);
                _valid.Reset();
            }
        }
    }
    public void Mixed(GameObject player)
    {
        if (!_playersMix.Contains(player))
        {
            _playersMix.Add(player);
            switch (player.GetComponentInChildren<Animator>().gameObject.name)
            {
                case "Ch_Character_Cat":
                    _valid.CatGood();
                    break;
                case "Ch_Character_Dog_No_EarsRig":
                    _valid.DogGood();
                    break;
                case "Ch_Character_Monkey":
                    _valid.MonkeyGood();
                    break;
            }
            if (_playersMix.Count == 3)
            {
                _onMixed.Invoke();
                _playersMix.Add(gameObject);
                _valid.Reset();
            }
        }
    }
    public void Sent()
    {

        _valid.CatGood();
        _valid.DogGood();    
        _valid.MonkeyGood();
        _onSent.Invoke();
    }
}
