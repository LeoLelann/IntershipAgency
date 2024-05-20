using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heat", menuName = "Pasteur/Heat", order = 0)]
public class SCHeat : ScriptableObject
{
    [SerializeField] private List<Heat> _heated = new List<Heat>();

    public List<Heat> Heated { get => _heated; set => _heated = value; }

}
[System.Serializable]
public class Heat
{
    [SerializeField] private Glassware.glasswareState[] _state = new Glassware.glasswareState[2];
    public Glassware.glasswareState[] State { get => _state; set => _state = value; }
}