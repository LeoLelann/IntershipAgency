using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

public class Dilution : Interactable
{
    [SerializeField]private UnityEvent _onInteractFailed;
    [SerializeField] private UnityEvent _onInteracted;
     [SerializeField] private UnityEvent _onAlreadyDiluted;
    [SerializeField] private UnityEvent _onTooDiluted;

    // Start is called before the first frame update
  
    [SerializeField]SCDilution _dilute;
    [SerializeField]private int _phase1=6;
    [SerializeField]private int _phase2=8;
    [SerializeField]private int _max=10;
    [SerializeField] TutoManager _tuto;

    private bool _diluting;
    private int _count;
    void Start()
    {
        _count = 0;
        _diluting = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void Interacted(GameObject player)
    {
       Glassware _playerGlassware= player.GetComponentInChildren<Glassware>();
        if (_playerGlassware != null)
        {
             if(_playerGlassware.GlasswareSt != Glassware.glasswareState.EMPTY)
            {
                if (_diluting == false)
                {
                    _diluting = true;
                    _phase1 = _dilute.Diluted.Find(x => x.State[0] == _playerGlassware.GlasswareSt).Phase1;
                    _phase2 = _dilute.Diluted.Find(x => x.State[0] == _playerGlassware.GlasswareSt).Phase2;
                    _max = _dilute.Diluted.Find(x => x.State[0] == _playerGlassware.GlasswareSt).Max;
                    _count++;
                }
                    _onInteracted?.Invoke();
                    switch (_count)
                    {
                        case int i when i <=_phase1:
                            _count++;
                            if (_count > _phase1)
                             {
                            _playerGlassware.SetGlasswareState(_dilute.Diluted.Find(x => x.State[0] == _playerGlassware.GlasswareSt).State[1]);
                            _onAlreadyDiluted?.Invoke();
                            }
                            break;
                        case int i when (i>_phase1&&i<=_phase2):
                            _count++;
                            if (_count > _phase2)
                            {
                            _playerGlassware.SetGlasswareState( Glassware.glasswareState.WATER);
                            _onTooDiluted?.Invoke();
                            }
                            break;
                        case int i when i > _phase2:
                            if (_count < _max)
                            {
                                _count++;
                            }
                            break;
                    
                }
                if (SceneManager.GetActiveScene().name == "Tutoriel 1")
                {
                    
                    if (_playerGlassware.GlasswareSt == Glassware.glasswareState.WATER)
                    {
                        _tuto.Diluted1(player);
                    }
                    if (_playerGlassware.GlasswareSt == Glassware.glasswareState.ACID_DILUTED)
                    {
                        _tuto.Diluted2(player);
                    }
                }
            } 
        }
        else
        {
            _onInteractFailed?.Invoke();
        }
        
    }
    public void ResetDilution()
    {
        _diluting = false;
        _count = 0;
    }
}
