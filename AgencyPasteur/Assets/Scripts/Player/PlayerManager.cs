using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject _p2;
    [SerializeField] private GameObject _p3;

    private string prefabName1 = "P1(Clone)";
    private string prefabName2 = "P2(Clone)";

    private bool p1;
    private bool p2;

    PlayerInputManager _playerInputManager;

    void Start()
    {
        p1 = false;
        p2 = false;

        _playerInputManager = GetComponent<PlayerInputManager>();
    }

    void Update()
    {
        if (!p1)
        {
            GameObject[] allObjects = FindObjectsOfType<GameObject>();
            foreach (GameObject obj in allObjects)
            {
                if (obj.name == prefabName1)
                {
                    p1 = true;
                    break;
                }
            }
        }
        if (p1)
        {
            PlayerInputManager.instance.playerPrefab = _p2;
        }

        if (!p2)
        {
            GameObject[] allObjects = FindObjectsOfType<GameObject>();

            foreach (GameObject obj in allObjects)
            {
                if (obj.name == prefabName2)
                {
                    p2 = true;
                    break;
                }
            }
        }
        if (p2)
        {
            PlayerInputManager.instance.playerPrefab = _p3;
        }
    }
}
