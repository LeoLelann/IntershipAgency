using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance => instance;

    public List<Glassware.glasswareState> Found { get => _found;}

    [SerializeField] private GameObject _book;
    private gamePhase _currentPhase;
    public enum gamePhase
    {
        PARTY_GAME_1,
        PARTY_GAME_2,
        PARTY_GAME_3,
        SHOOTER,
        MENUS,
    }
    [SerializeField] private List<AddToBook> _floatingPages = new List<AddToBook>();
    [SerializeField] private List<Glassware.glasswareState> _found = new List<Glassware.glasswareState>();
    [SerializeField]private List<Glassware.glasswareState> _neededGlasswareP1=new List<Glassware.glasswareState>();
    [SerializeField]private List<Glassware.glasswareState> _neededGlasswareP2=new List<Glassware.glasswareState>();
    [SerializeField] private List<Glassware.glasswareState> _neededGlasswareP3=new List<Glassware.glasswareState>();
    private List<Glassware.glasswareState> _neededGlasswareType=new List<Glassware.glasswareState>();
    [SerializeField] private GameObject _displayNGT;
    #region Singleton
    private void InitSingleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion
    private void Awake()
    {
        InitSingleton();
    }
    // Start is called before the first frame update
    void Start()
    {
        _found.Clear();
        _found.Add(Glassware.glasswareState.EMPTY);
        _found.Add(Glassware.glasswareState.ACID);
        _found.Add(Glassware.glasswareState.WATER);
        _found.Add(Glassware.glasswareState.STARCH);
        _found.Add(Glassware.glasswareState.TALC);
        _found.Add(Glassware.glasswareState.TRASH);
        if (_currentPhase == gamePhase.PARTY_GAME_1)
        {
            _neededGlasswareType = _neededGlasswareP1;
        }
        else if (_currentPhase == gamePhase.PARTY_GAME_2)
        {
           foreach(Glassware.glasswareState needed in _neededGlasswareP1)
            {
                _found.Add(needed);
            }
            _neededGlasswareType = _neededGlasswareP2;
        }
        else if (_currentPhase == gamePhase.PARTY_GAME_3)
        {
            foreach (Glassware.glasswareState needed in _neededGlasswareP1)
            {
                _found.Add(needed);
            }
            foreach (Glassware.glasswareState needed in _neededGlasswareP2)
            {
                _found.Add(needed);
            }
            _neededGlasswareType = _neededGlasswareP3;
        }
        DisplayRightComponent();
    }
    public void AddElement(Glassware.glasswareState state)
    {
        _found.Add(state);
        foreach(AddToBook pages in _floatingPages)
        {
            if (pages.GlasswareState == state)
            {
                pages.gameObject.SetActive(true);
            }
        }
    }
   
    private void DisplayRightComponent()
    {
        for(int i =0;i<_neededGlasswareType.Count;i++)
        {
            _displayNGT.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
