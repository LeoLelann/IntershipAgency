using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    [SerializeField] private UnityEvent _onStartGame;
    [SerializeField] private UnityEvent _onEndGamePerfect;
    [SerializeField] private UnityEvent _onEndGameGood;
    [SerializeField] private UnityEvent _onEndGameBad;
    [SerializeField] private int _goalNbrRemedy;
    private int _currentNbrRemedy;
    public static GameManager Instance => instance;

    public List<Glassware.glasswareState> Found { get => _found;}
    public float Timer1 { get => _timer;}
    public int GoalNbrRemedy { get => _goalNbrRemedy; set => _goalNbrRemedy = value; }

    [SerializeField] private GameObject _book;
    private gamePhase _currentPhase;
    public enum gamePhase
    {
        PARTY_GAME,
        TUTO,
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
        _currentNbrRemedy = 0;
        StartCoroutine(Timer());
    }
    private void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().name!="Tuto"|| SceneManager.GetActiveScene().name != "Menu")
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
        switch (_currentNbrRemedy)
        {
            case int i when i <=_goalNbrRemedy / 2:
                _onEndGameBad.Invoke();
                break;
            case int i when i <= _goalNbrRemedy *8/10:
                _onEndGameBad.Invoke();
                break;
            case int i when i > _goalNbrRemedy *8/10:
                _onEndGameBad.Invoke();
                break;
        }
        Debug.Log("Fin de game");
    }
}
