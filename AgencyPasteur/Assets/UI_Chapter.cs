using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Chapter : MonoBehaviour
{
    RectTransform rect;
    [SerializeField] List<Page> _pages = new List<Page>();
    [SerializeField] GameObject _notification;
    private void Start()
    {
        rect = GetComponent<RectTransform>();
        _notification.SetActive(false);
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
        if (_pages.FindAll(x => x.IsNew == true).Count > 0) 
        {
            _notification.SetActive(true);
        }
        else
        {
            _notification.SetActive(false);
        }
    }
}
