using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Dilution : Interactable
{
     public UnityEvent OnInteractFailed;
     public UnityEvent OnInteracted;
     public UnityEvent OnAlreadyDiluted;
     public UnityEvent OnTooDiluted;

    // Start is called before the first frame update
    private Glassware.glasswareState _state;
   private Glassware.glasswareState _experimentState;
    SCDilution _dilute;
    [SerializeField]private int _phase1=6;
    [SerializeField]private int _phase2=8;
    [SerializeField]private int _max=10;
    private bool _diluting;
    private int _count;
    void Start()
    {
        _state=Glassware.glasswareState.WATER;
        _experimentState=Glassware.glasswareState.EMPTY;
        _count = 0;
        _diluting = false;
        SCDilution path = Resources.Load<SCDilution>("ScriptableObject/Dilution");
        _dilute = path;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void Interacted(GameObject player)
    {
        if ( player.transform.GetComponentInChildren<Glassware>() != null)
        {
             if(player.transform.GetComponentInChildren<Glassware>().GlasswareSt != Glassware.glasswareState.EMPTY)
            {
                if (_diluting == false)
                {
                    _diluting = true;
                    _phase1 = _dilute.Diluted.Find(x => x.State[0] == player.transform.GetComponentInChildren<Glassware>().GlasswareSt).Phase1;
                    _phase2 = _dilute.Diluted.Find(x => x.State[0] == player.transform.GetComponentInChildren<Glassware>().GlasswareSt).Phase2;
                    _max = _dilute.Diluted.Find(x => x.State[0] == player.transform.GetComponentInChildren<Glassware>().GlasswareSt).Max;
                    _count++;
                }
                    OnInteracted?.Invoke();
                    switch (_count)
                    {
                        case int i when i <=_phase1:
                            _count++;
                            if (_count > _phase1)
                            {
                               player.GetComponentInChildren<Glassware>().SetGlasswareState(_dilute.Diluted.Find(x => x.State[0] == player.transform.GetComponentInChildren<Glassware>().GlasswareSt).State[1]);
                            OnAlreadyDiluted?.Invoke();
                            }
                            break;
                        case int i when (i>_phase1&&i<=_phase2):
                            _count++;
                            if (_count > _phase2)
                            {
                                player.GetComponentInChildren<Glassware>().SetGlasswareState( Glassware.glasswareState.WATER);
                            OnTooDiluted?.Invoke();
                            }
                            break;
                        case int i when i > _phase2:
                            if (_count < _max)
                            {
                                _count++;
                            }
                            break;
                    
                }
            }
            
        }
        else
        {
            OnInteractFailed?.Invoke();
        }
    }
}
