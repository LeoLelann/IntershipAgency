using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Save : MonoBehaviour
{
    private string _savePath;
    private bool _levelFinish;
    private void Awake()
    {
        _savePath = Application.persistentDataPath;
    }

    public void SaveLevel()
    {
        BinaryWriter writer = new BinaryWriter(File.Open(_savePath, FileMode.Create));
    }
}
