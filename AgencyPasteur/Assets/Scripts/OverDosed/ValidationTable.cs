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

    [SerializeField] private List<Glassware.glasswareState> ToFind = new List<Glassware.glasswareState>();
    [SerializeField]private List<Glassware.glasswareState> Found=new List<Glassware.glasswareState>();
    [SerializeField]private List<Glassware.glasswareState> FoundImportant=new List<Glassware.glasswareState>();
    [SerializeField] private UI_Completion _completion;
    private Glassware _glassware;
    [SerializeField] TutoManager _tuto;

    private void Start()
    {
        _glassware = GetComponentInChildren<Glassware>();
        _completion.ResultMax = ToFind.Count;

        if(GameManager.Instance != null) GameManager.Instance.GoalNbrRemedy = ToFind.Count;
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
            Found.Add(_glassware.GlasswareSt);
        }
        if (ToFind.Contains(_glassware.GlasswareSt)&&!FoundImportant.Contains(_glassware.GlasswareSt))
        {
            
            FoundImportant.Add(_glassware.GlasswareSt);
            _completion.UpdateCount(FoundImportant.Count);

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
        if (FoundImportant.Count == ToFind.Count&&SceneManager.GetActiveScene().name!="Tutoriel 1")
        {
            GameManager.Instance.EndGame();
        }
    }

}
