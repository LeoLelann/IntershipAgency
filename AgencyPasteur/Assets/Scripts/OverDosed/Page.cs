using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Page : MonoBehaviour
{
    [SerializeField] bool _isLocked;

    public bool IsLocked
    {
        get => _isLocked;
        set
        {
            _isLocked = value;
            if (_isLocked)
            {
                GetComponent<Image>().color = Color.black;
            }
            else
            {
                GetComponent<Image>().color = Color.white;
            }
        }
    }

    void Start()
    {
        IsLocked = _isLocked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
