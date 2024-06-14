using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static CharacterSelectionSO;

public class CharacterSetup : MonoBehaviour
{
    [Serializable]
    struct ModelAssociation
    {
        public Character Character;
        public GameObject Model;
    }

    [SerializeField] ModelAssociation[] _conf;
    [SerializeField] PlayerInput _input;
    [SerializeField] CharacterSelectionSO _s;

    private void Awake()
    {
        string device = _input.actions.devices.Value[0].name;
        Character selected = _s.Selection.First(i => i.Item2 == device).Item1;

        _conf.First(i => i.Character == selected).Model.SetActive(true);

        Debug.Log(_input.actions.devices.Value[0].name);
    }
}
