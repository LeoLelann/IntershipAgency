using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoosedPlayer : MonoBehaviour
{
    [SerializeField] Sprite _defaultSprite;
    [SerializeField] Sprite _selectedSprite1;
    [SerializeField] Sprite _selectedSprite2;
    [SerializeField] Sprite _selectedSprite3;

    void OnChangePlayer1(int id)
    {
        switch (id)
        {
            case 0:
                GetComponent<Image>().sprite = _selectedSprite1;
                break;
            case 1:
                GetComponent<Image>().sprite = _selectedSprite2;
                break;
            case 2:
                GetComponent<Image>().sprite = _selectedSprite3;
                break;
        }
    }

    void ResetSprite()
    {
        GetComponent<Image>().sprite = _defaultSprite;
    }
}
