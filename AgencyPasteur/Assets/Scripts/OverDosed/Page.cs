using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Page : MonoBehaviour
{
    [SerializeField] private UnityEvent _onUnlock;
    [SerializeField] bool _isLocked;
    private Image _image;
    
    
    public bool IsLocked
    {
        get => _isLocked;
        set
        {
            _isLocked = value;
            if (_isLocked)
            {
                _image.color = Color.black;
            }
            else
            {
                _onUnlock.Invoke();
                _image.color = Color.white;
            }
        }
    }

    void Start()
    {
        _image = GetComponent<Image>();
        IsLocked = _isLocked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
