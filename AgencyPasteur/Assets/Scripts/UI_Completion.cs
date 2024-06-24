using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Completion : MonoBehaviour
{
    [SerializeField] TMP_Text _UITimer;
    private int _resultMax;
    private int _currentResult;

    public int ResultMax { get => _resultMax; set => _resultMax = value; }

    void Start()
    {
        _currentResult = 0;
    }
    public void UpdateCount(int currentCount)
    {
        _currentResult = currentCount;
        _UITimer.text = ($"{_currentResult}");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
