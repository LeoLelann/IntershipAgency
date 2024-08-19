using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ValidationTable : Interactable
{
    [SerializeField]private UnityEvent _onValidate;
    [SerializeField]private UnityEvent _onInvalidate;
    [SerializeField] private UnityEvent _onShowMissingRemedy;

    [SerializeField] private List<Glassware.glasswareState> _toFind = new List<Glassware.glasswareState>();
    [SerializeField]private List<Glassware.glasswareState> _found=new List<Glassware.glasswareState>();
    [SerializeField]private List<Glassware.glasswareState> _foundImportant=new List<Glassware.glasswareState>();
    [SerializeField] private UI_Completion _completion;
    private Glassware _glassware;
    [SerializeField] TutoManager _tuto;

    public List<Glassware.glasswareState> Found { get => _found;}

    private void Start()
    {
        _glassware = GetComponentInChildren<Glassware>();
        _completion.ResultMax = _toFind.Count;

        if(GameManager.Instance != null) GameManager.Instance.GoalNbrRemedy = _toFind.Count;
        _completion.UpdateCount(0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.CompareTag("Glassware") && collision.transform.parent == null && _glassware == null )
        {
            if (collision.transform.GetComponent<Glassware>().GlasswareSt != Glassware.glasswareState.EMPTY)
            {
                collision.transform.parent = transform;
                _glassware = GetComponentInChildren<Glassware>();
                collision.transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
                collision.transform.rotation = Quaternion.Euler(270, 0, 0);
                collision.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                Validation();
            }
            
        }
    }
    public override void Interacted(GameObject player)
    {
        _glassware=GetComponentInChildren<Glassware>();
        Glassware playerGlassware =player.GetComponentInChildren<Glassware>();
        if (_glassware != null && playerGlassware == null)
        {
            _glassware.Interacted(player);
        }
        else if (playerGlassware != null && _glassware == null && playerGlassware.GlasswareSt != Glassware.glasswareState.EMPTY)
        {
            player.GetComponent<Player>().Anim.SetBool("IsHolding", false);
            player.GetComponent<Player>().Anim.SetBool("IsPuttingDown", true);
            playerGlassware.transform.parent = transform;
            _glassware = playerGlassware;
            _glassware.transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
            _glassware.transform.rotation = Quaternion.Euler(270, 0, 0);
            _glassware.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            Validation();
        }
    }

    public void Validation()
    {
        if (!Found.Contains(_glassware.GlasswareSt))
        {
            _onValidate?.Invoke();
            if (SceneManager.GetActiveScene().name != "Tutoriel 1")
            {
                GameManager.Instance.AddElement(_glassware.GlasswareSt);
            }
            _found.Add(_glassware.GlasswareSt);
        }
        if (_toFind.Contains(_glassware.GlasswareSt)&&!_foundImportant.Contains(_glassware.GlasswareSt))
        {
            Debug.Log("aad");
            _foundImportant.Add(_glassware.GlasswareSt);
            _completion.UpdateCount(_foundImportant.Count);

            if (SceneManager.GetActiveScene().name == "Tutoriel 1")
            {
                Debug.Log("Feur");
                _tuto.Sent();
            }
        }
        else
        {
            _onInvalidate?.Invoke();
        }
        Destroy(_glassware.gameObject);
        if (_foundImportant.Count == _toFind.Count&&SceneManager.GetActiveScene().name!="Tutoriel 1")
        {
            Debug.Log(_foundImportant.Count);
            Debug.Log(_toFind.Count);
            GameManager.Instance.EndGame();
        }
    }

}
