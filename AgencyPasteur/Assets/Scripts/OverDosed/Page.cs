using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Page : MonoBehaviour
{
    [SerializeField] private UnityEvent _onUnlock;
    [SerializeField] bool _isLocked;
    [SerializeField] bool _isNew;
    [SerializeField]private Image _image;
    [SerializeField]private UI_Chapter chapter;
    
    
    public bool IsLocked
    {
        get => _isLocked;
        set
        {
            _isLocked = value;
            if (_isLocked)
            {
                _image.color = new Color(1,1,1,0);
            }
            else
            {
                _onUnlock.Invoke();
                _image.color = new Color(1, 1, 1, 1);
            }
        }
    }

    public bool IsNew {
        get => _isNew;
        set
        {
            _isNew = value;
        }
    }

    private void OnEnable()
    {
        IsLocked = _isLocked;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isNew)
        {
            _isNew = false;
            chapter.NotifyUpdate();
        }
    }
}
