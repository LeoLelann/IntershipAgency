using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvaManager : MonoBehaviour
{
    [SerializeField] private Canvas _mainCanva;
    [SerializeField] private Canvas _levelCanva;
    [SerializeField] private Canvas _introCanva;

    [SerializeField] private Button _dftMain;
    [SerializeField] private Button _dftLvl;
    [SerializeField] private Button _dftIntroLvl;

    //[SerializeField] private EventSystem _eS;
    //public void OnChangeCanva()
    //{
    //    EventSystem.current.SetSelectedGameObject(null);
    //    if (!_mainCanva.gameObject.activeInHierarchy)
    //    {
    //        if (!_levelCanva.gameObject.activeInHierarchy)
    //        {
    //            if (!_introCanva.gameObject.activeInHierarchy)
    //            {
    //                Debug.Log("Probleme");
    //            }
    //            else
    //            {
    //                EventSystem.current.SetSelectedGameObject(_dftIntroLvl.gameObject);
    //            }
    //        }
    //        else
    //        {
    //            EventSystem.current.SetSelectedGameObject(_dftLvl.gameObject);
    //        }
    //    }
    //    else
    //    {
    //        EventSystem.current.SetSelectedGameObject(_dftMain.gameObject);
    //    }
    //}
}
