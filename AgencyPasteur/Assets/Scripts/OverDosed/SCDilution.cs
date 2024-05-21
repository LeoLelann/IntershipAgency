using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dilution", menuName = "Pasteur/Dilution", order = 0)]
public class SCDilution : ScriptableObject
{
    [SerializeField] private List<Dilute> _diluted = new List<Dilute>();

    public List<Dilute> Diluted { get => _diluted; set => _diluted = value; }

}
[System.Serializable]
public class Dilute
{
    [SerializeField] private Glassware.glasswareState[] _state = new Glassware.glasswareState[2];
    [SerializeField] private int _phase1;
    [SerializeField] private int _phase2;
    [SerializeField] private int _max;
    public Glassware.glasswareState[] State { get => _state; set => _state = value; }
    public int Phase1 { get => _phase1; set => _phase1 = value; }
    public int Phase2 { get => _phase2; set => _phase2 = value; }
    public int Max { get => _max; set => _max = value; }
}