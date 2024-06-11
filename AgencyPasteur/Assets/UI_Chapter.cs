using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Chapter : MonoBehaviour
{
    RectTransform rect;
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
}
