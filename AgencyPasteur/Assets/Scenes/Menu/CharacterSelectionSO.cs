using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="CharacterSelection")]
public class CharacterSelectionSO : ScriptableObject
{
    
    public enum Character { Oliv, Lesha, Britney}

    [SerializeField] Character[] _selection;

    public Character[] Selection { get => _selection; }

    public void SendSelection(Character[] result)
    {
        _selection = result;
    }


}
