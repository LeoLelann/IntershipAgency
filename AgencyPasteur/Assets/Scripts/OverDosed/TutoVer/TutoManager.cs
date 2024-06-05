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

    private bool _firstElementTaken;
    private bool _heatFirstElement;
    private bool _dilutedFirstElement;
    private bool _dilutedSecondElement;
    private bool _mixedFirstTime;
    private void Start()
    {
        _onStartTuto.Invoke();
    }
    public void TookElement()
    {
        _onGotElement.Invoke();
    }
    public void Heated()
    {
        _onHeated.Invoke();
    }
    public void Diluted1()
    {
        _onDiluted1Element.Invoke();
    }
    public void Diluted2()
    {
        _onDiluted2Element.Invoke();
    }
    public void Mixed()
    {
        _onMixed.Invoke();
    }
    public void Sent()
    {
        _onSent.Invoke();
    }
    public bool FirstElementTaken { get => _firstElementTaken; set => _firstElementTaken = value; }
    public bool HeatFirstElement { get => _heatFirstElement; set => _heatFirstElement = value; }
    public bool DilutedFirstElement { get => _dilutedFirstElement; set => _dilutedFirstElement = value; }
    public bool DilutedSecondElement { get => _dilutedSecondElement; set => _dilutedSecondElement = value; }
    public bool MixedFirstTime { get => _mixedFirstTime; set => _mixedFirstTime = value; }
}
