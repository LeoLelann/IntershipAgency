using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Mix",menuName ="Pasteur/Mix",order =0)]
public class SCMix : ScriptableObject
{
    [SerializeField]private List<Mix> _mixed = new List<Mix>();
    public List<Mix> Mixed { get => _mixed; set => _mixed = value; }
}
[System.Serializable]
public class Mix
{
    [SerializeField] private Glassware.glasswareState[] _state = new Glassware.glasswareState[3];
    public Glassware.glasswareState[] State { get => _state; set => _state = value; }
}
