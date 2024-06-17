using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValidateTuto : MonoBehaviour
{
    [SerializeField] Image _singe;
    [SerializeField] Image _chat;
    [SerializeField] Image _chien;
    [SerializeField] Sprite _chienO;
    [SerializeField] Sprite _chienX;
    [SerializeField] Sprite _chatO;
    [SerializeField] Sprite _chatX;
    [SerializeField] Sprite _singeO;
    [SerializeField] Sprite _singeX;

    private void Start()
    {
        Reset();
    }
    // Start is called before the first frame update
    public void MonkeyGood()
    {
        _singe.sprite = _singeO;
    }
    public void DogGood()
    {
        _chien.sprite = _chienO;
    }
    public void CatGood()
    {
        _chat.sprite = _chatO;
    }
    // Update is called once per frame
    public void Reset()
    {
        _singe.sprite = _singeX;
        _chat.sprite = _chatX;
        _chien.sprite = _chienX;
    }
}
