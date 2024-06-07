using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SpawnerElement : Interactable
{
    [SerializeField] UnityEvent _onTakeGlassware;
    [SerializeField]private UnityEvent _onTakeFrom;
    [SerializeField]private UnityEvent _onSnapGlassware;
    [SerializeField] private List<Material> _elementIcon;
    [SerializeField] private MeshRenderer _label;
    [SerializeField] GameObject _ressource;
    private Glassware _glassware;
    [SerializeField] int _limit;
    [SerializeField] TutoManager _tuto;
    List<GameObject> _players = new List<GameObject>();
    public enum Elements
    {
        TALC,
        ACID,
        STARCH
    };
    [SerializeField] private Elements element;

    private void Start()
    {
        _glassware = GetComponentInChildren<Glassware>();
        OnStateValueChange(element);
    }
    private void OnStateValueChange(Elements state)
    {
        switch (state)
        {
            case (Elements.ACID):                
                    _label.material=_elementIcon[1];       
                break;
            case (Elements.STARCH):

                _label.material = _elementIcon[0];
                
                break;
            case (Elements.TALC):
                
                    _label.material.color = Color.blue;
                
                break;
         }
    }
    public override void Interacted(GameObject player)
    {
        _glassware = GetComponentInChildren<Glassware>();
        Glassware playerGlassware = player.GetComponentInChildren<Glassware>();
        if (playerGlassware == null)
        {
            if (_glassware != null)
            {
                _glassware.Interacted(player);
                _onTakeFrom?.Invoke();
            }
            else
            {
                if(SceneManager.GetActiveScene().name=="Tutoriel 1")
                {
                    Debug.Log("ahzipfhzlfmq");
                    if (!_players.Contains(player))
                    {
                        _players.Add(player);
                    }
                    if (_players.Count == 3)
                    {
                        _tuto.TookElement();
                        _players.Add(gameObject);
                    }
                }
                _onTakeGlassware?.Invoke();
                GameObject glassware = Instantiate(_ressource, transform.position, Quaternion.identity);
                switch (element)
                {
                    case Elements.TALC:
                        glassware.GetComponent<Glassware>().SetGlasswareState(Glassware.glasswareState.TALC);
                        break;
                    case Elements.STARCH:
                        glassware.GetComponent<Glassware>().SetGlasswareState(Glassware.glasswareState.STARCH);
                        break;
                    case Elements.ACID:
                        glassware.GetComponent<Glassware>().SetGlasswareState(Glassware.glasswareState.ACID);
                        break;
                }
                glassware.GetComponent<Glassware>().Interacted(player);
            }
        }
        else if (playerGlassware != null && _glassware == null)
        {
            _onSnapGlassware?.Invoke();
            playerGlassware.transform.parent = transform;
            _glassware = GetComponentInChildren<Glassware>();
            _glassware.transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
            _glassware.transform.rotation = Quaternion.Euler(270, 0, 0);
            _glassware.transform.GetComponent<Collider>().enabled = false;
            _glassware.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Glassware>() != null && collision.transform.parent == null && _glassware == null)
        {
            _onSnapGlassware?.Invoke();
            collision.transform.parent = transform;
            collision.transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
            collision.transform.localRotation = Quaternion.Euler(270, 0, 0);
            collision.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            collision.transform.GetComponent<Collider>().enabled = false;
        }
    }
}
