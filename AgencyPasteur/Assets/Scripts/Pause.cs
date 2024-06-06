using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject _pauseMenu;
    public void SetPause()
    {
        if(!_pauseMenu.activeInHierarchy)
        {
            Time.timeScale = 0;
            _pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            _pauseMenu.SetActive(false);
        }
    }
}
