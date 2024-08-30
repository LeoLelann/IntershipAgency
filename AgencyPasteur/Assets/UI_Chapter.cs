using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Chapter : MonoBehaviour
{
    RectTransform rect;
    [SerializeField] List<Page> _pages = new List<Page>();
    [SerializeField] GameObject _notification;
    [SerializeField] TMP_Text _notifCount;
    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }
    public void Up()
    {
        Debug.Log(rect.position.y);
        rect.position = new Vector3(rect.position.x,rect.position.y+20,rect.position.z);
    }
    public void Down()
    {
        Debug.Log(rect.position.y);
        rect.position= new Vector3(rect.position.x, rect.position.y - 20, rect.position.z);

    }
    public void NotifyUpdate()
    {
        for(int i = 0; i < _pages.Count; i++)
        {
            if (_pages[i].IsNew == true)
            {
                Debug.Log(i);
            }
        }
        if (_pages.FindAll(x => x.IsNew == true).Count > 0)
        {
            Debug.Log("lancé");
            _notification.SetActive(true);
            _notifCount.text = $"{_pages.FindAll(x => x.IsNew == true).Count}";
        }
        else
        {
            Debug.Log("annulé");
            _notification.SetActive(false);
        }
    }
}
