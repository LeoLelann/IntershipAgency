using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorController : MonoBehaviour
{
    [SerializeField] Image _img;
    [SerializeField] Color _activatedColor;
    [SerializeField] Color _disabledColor;

    public void ActivateColor()
    {
        _img.color = _activatedColor;
    }

    public void DisabledColor()
    {
        _img.color = _disabledColor;
    }

}
