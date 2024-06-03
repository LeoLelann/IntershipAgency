using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    [SerializeField] private UnityEvent _onStartGame;
    [SerializeField] private UnityEvent _onEndGame;
    public static GameManager Instance => instance;

    public List<Glassware.glasswareState> Found { get => _found;}

    [SerializeField] private GameObject _book;
    private gamePhase _currentPhase;
    public enum gamePhase
    {
        PARTY_GAME,
        MENUS,
    }
    [SerializeField] private List<AddToBook> _floatingPages = new List<AddToBook>();
    [SerializeField] private List<Glassware.glasswareState> _found = new List<Glassware.glasswareState>();
    private List<Glassware.glasswareState> _neededGlasswareType=new List<Glassware.glasswareState>();
    [SerializeField] private GameObject _displayNGT;
    [SerializeField] private float _timer;
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
        StartCoroutine(Timer());
    }
    private void OnLevelWasLoaded(int level)
    {
        if (_currentPhase==gamePhase.PARTY_GAME)
        {
            StartCoroutine(Timer());
        }
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
   
    IEnumerator Timer()
    {
        float time = 0;
        while (time < _timer)
        {
            time += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        EndGame();
        yield return null;
    }
    public void EndGame()
    {
        StopAllCoroutines();
        _onEndGame.Invoke();
        Debug.Log("Fin de game");
    }
}
