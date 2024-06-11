using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenOnSelect : MonoBehaviour
{
    public bool _isValidate { get; private set;}
    private bool _isChoosed;
    [SerializeField] GameObject _lockText;

    private void Start()
    {
        _isValidate = false;
        _isChoosed = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_isChoosed)
        {
            _lockText.SetActive(true);
            _isValidate = true;
            _isChoosed = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _lockText.SetActive(false);
        _isValidate = false;
        _isChoosed = false;
    }
}
