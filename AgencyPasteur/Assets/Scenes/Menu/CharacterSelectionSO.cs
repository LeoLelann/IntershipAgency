using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="CharacterSelection")]
public class CharacterSelectionSO : ScriptableObject
{
    
    public enum Character { Oliv, Lesha, Britney}

    [SerializeField] (Character, string)[] _selection;

    public (Character, string)[] Selection { get => _selection; }

    public void SendSelection((Character, string)[] result)
    {
        _selection = result;
    }


}
