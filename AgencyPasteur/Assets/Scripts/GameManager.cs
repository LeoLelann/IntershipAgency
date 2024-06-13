using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    [SerializeField] private UnityEvent _onStartGame;
    [SerializeField] private UnityEvent _onEndGamePerfect;
    [SerializeField] private UnityEvent _onEndGameGood;
    [SerializeField] private UnityEvent _onEndGameBad;
    [SerializeField] private int _goalNbrRemedy;
    [SerializeField] private GameObject _cover;
    [SerializeField] private AdjustVolume _renderVolume;
    [SerializeField] private EndOfLevelDoor _door;
    private int _currentNbrRemedy;
   [SerializeField] private GameObject _UI_cinematique;
   [SerializeField] private Cinematique _cine1;
    public static GameManager Instance => instance;

    public List<Glassware.glasswareState> Found { get => _found;}
    public float Timer1 { get => _timer;}
    public int GoalNbrRemedy { get => _goalNbrRemedy; set => _goalNbrRemedy = value; }

    [SerializeField] private GameObject _book;
    private Player[] players; 
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
    [SerializeField] private Timer _UITimer;
    LiftGammaGain liftGammaGain;
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
        players = FindObjectsOfType<Player>();
        Debug.Log(SceneManager.GetActiveScene().name);
        _currentNbrRemedy = 0;
        StartCoroutine(Timer());
    }
    private void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().name!="Tutoriel 1"|| SceneManager.GetActiveScene().name != "MainMenu")
        {
            StartCoroutine(Timer());
        }
    }
    public void AddElement(Glassware.glasswareState state)
    {if (SceneManager.GetActiveScene().name != "Tutoriel 1")
        {
            _found.Add(state);
            foreach (AddToBook pages in _floatingPages)
            {
                if (pages.GlasswareState == state)
                {
                    _cover.SetActive(true);
                    pages.gameObject.SetActive(true);
                }
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
        foreach(Player p in players)
        {
            p.Anim.SetBool("Lost", true);
        }
        yield return new WaitForSeconds(1);
        foreach(Player p in players)
        {
            p.Anim.SetBool("Lost", false);
        }
        EndGame();
        yield return null;
    }
    public void EndGame()
    {
        if(SceneManager.GetActiveScene().name!="Tutoriel 1")
        {
            StopAllCoroutines();
            if (_currentNbrRemedy == _goalNbrRemedy) 
            {
                StartCoroutine(GoodEnd());
            }
            _renderVolume.AdjustGamma(-0.1f);
            _renderVolume.AdjustVignette(new Vector2(0.65f, 0.8f));
            foreach (trigerObject i in FindObjectsOfType<trigerObject>())
            {
                Debug.Log(i.Player.name);
                i.Player.range = null;
                i.gameObject.SetActive(false);
            }
            _UITimer.Stop();
            StopAllCoroutines();
            _door.OnEnd();
            _UI_cinematique.SetActive(true);
            _cine1.NextSlide();
        }    
    }
    IEnumerator GoodEnd()
    {
        foreach (Player p in players)
        {
            p.Anim.SetBool("Win", true);
        }
        yield return new WaitForSeconds(1);
        foreach (Player p in players)
        {
            p.Anim.SetBool("Win", false);
        }
    }
}
