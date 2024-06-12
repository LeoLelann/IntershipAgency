using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterSelectionSO;

public class TokenOnSelect : MonoBehaviour
{
    [SerializeField] Character _playerRepresented;

    public Character PlayerRepresented { get => _playerRepresented;  }
}
