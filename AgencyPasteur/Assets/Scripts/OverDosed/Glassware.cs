using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Glassware : Interactable
{
    [SerializeField] private UnityEvent _onThrown;
    [SerializeField] private UnityEvent _onDrop;
    [SerializeField] private UnityEvent _onChangeState;
    [SerializeField] private UnityEvent _onPicked;
    [SerializeField] private UnityEvent _onCollisionWhenThrown;
    [SerializeField] private UnityEvent _onBecameTrash;
    [SerializeField] private UnityEvent _onBecameWater;

    public enum glasswareState
    {
        EMPTY,
        WATER,
        ACID,
        TALC,
        STARCH,
        RABIES_VIRUS,
        SODIUM_CHLORIDE,
        POWDER,
        VALANCE,
        ACID_STARCH,
        ACID_TALC,
        THICK_POWDER,
        ACID_STARCH_DILUTED,
        ACID_TALC_DILUTED,
        THICK_POWDER_DILUTED,
        STARCH_DILUTED,
        TALC_DILUTED,
        ACID_DILUTED,
        DILUTED_SODIUM_CHLORIDE,
        HEATED_ACID,
        HEATED_TALC,
        HEATED_STARCH,
        HEATED_POWDER,
        HEATED_ACID_STARCH,
        HEATED_ACID_TALC,
        HEATED_THICK_POWDER,
        HEATED_ACID_STARCH_DILUTED,
        HEATED_ACID_TALC_DILUTED,
        HEATED_THICK_POWDER_DILUTED,
        HEATED_STARCH_DILUTED,
        HEATED_TALC_DILUTED,
        HEATED_ACID_DILUTED,
        RABIES_VACCINE,
        DIRTY,
        TRASH
    };
    private bool isThrown;
    private Transform _parentTransform;
    private Rigidbody _rgbd;
    [SerializeField] private float _throwPower=2;
    private Collider _collider;
    [SerializeField]private MeshRenderer _meshRend;
    [SerializeField]private glasswareState _glasswareSt=glasswareState.EMPTY;

    public glasswareState GlasswareSt { get => _glasswareSt; }

    private void Awake()
    {
        
        _rgbd = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }
    private void Start()
    {
        isThrown = false;
        OnStateValueChange(_glasswareSt);
    }

    public void Thrown()
    {
        _onThrown?.Invoke();
        isThrown = true;
        _rgbd.constraints = RigidbodyConstraints.None;
        transform.position += new Vector3(0, 0.5f, 0);
        _rgbd.velocity = new Vector3(transform.parent.transform.forward.x * _throwPower, 0.5f, transform.parent.transform.forward.z * _throwPower);
        transform.parent = null;
        _collider.enabled = true;
    }
    public void Drop()
    {

        _onDrop?.Invoke();
        transform.parent = null;
        _rgbd.constraints = RigidbodyConstraints.None;
        _collider.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isThrown)
        {
            isThrown = false;
            _onCollisionWhenThrown?.Invoke();

        }
    }

    public override void Interacted(GameObject player)
    {
        if (player.transform.GetComponentInChildren<Glassware>()==null&&(transform.parent==null||transform.parent.GetComponent<Player>()==null)) 
        {
            if (player.GetComponent<Player>())
            {
                StartCoroutine(StartHolding(player.GetComponent<Player>()));
            }
            else
            {
                _onPicked?.Invoke();
                transform.rotation = Quaternion.Euler(270, 0, 0);
                transform.parent = player.transform;
                transform.localPosition = new Vector3(0, 0f, 0.5f);
                _rgbd.constraints = RigidbodyConstraints.FreezeAll;
                _parentTransform = GetComponentInParent<Transform>();
                _collider.enabled = false;
                player.GetComponent<Player>().range = null;
            }
        }
    }
    IEnumerator StartHolding(Player player)
    {
        player.Anim.SetBool("IsGrabbing", true);
        player.Anim.SetBool("IsPuttingDown", false);
        yield return new WaitForSeconds(0.2f);
        player.Anim.SetBool("IsHolding", true);
        player.Anim.SetBool("IsThrowing", false);
        player.Anim.SetBool("IsGrabbing", false);
        _onPicked?.Invoke();
        transform.rotation = Quaternion.Euler(270, 0, 0);
        transform.parent = player.transform;
        transform.localPosition = new Vector3(0, 0f, 0.5f);
        _rgbd.constraints = RigidbodyConstraints.FreezeAll;
        _parentTransform = GetComponentInParent<Transform>();
        _collider.enabled = false;
        player.GetComponent<Player>().range = null;

    }
    public void SetGlasswareState(glasswareState state)
    {
        _glasswareSt = state;
        if (SceneManager.GetActiveScene().name != "Tutoriel 1")
        {
            GameManager.Instance.AddElement(state);
        }
        OnStateValueChange(_glasswareSt);
    }
    private void OnStateValueChange(glasswareState state)
    {
        if(state!=glasswareState.EMPTY&&state!=glasswareState.WATER&&state!=glasswareState.TRASH&&state!=glasswareState.ACID&& state != glasswareState.STARCH&& state != glasswareState.RABIES_VIRUS&& state != glasswareState.SODIUM_CHLORIDE&& state != glasswareState.POWDER)
        {
            _onChangeState?.Invoke();
        }
        if(!_meshRend.gameObject.activeInHierarchy){
            _meshRend.gameObject.SetActive(true);
        }
        switch (state)
        {
            case (glasswareState.EMPTY):
                _meshRend.gameObject.SetActive(false);
                break;
            case (glasswareState.ACID):
                _meshRend.material.color = new Color(1,0.9f,0);
                break;
            case (glasswareState.STARCH):
                _meshRend.material.color = new Color(1, 0.75f, 0.8f); ;
                break;
            case (glasswareState.TALC):
                _meshRend.material.color = Color.blue;
                break;
            case (glasswareState.HEATED_ACID):
                _meshRend.material.color =  new Color(1.0f, 0.64f, 0.0f);
                break;
            case (glasswareState.HEATED_STARCH):
                _meshRend.material.color = Color.red;
                break;
            case (glasswareState.HEATED_TALC):
                _meshRend.material.color = new Color(0.5f,0,0.5f); ;
                break;
            case (glasswareState.TRASH):
                _onBecameTrash.Invoke();
                _meshRend.material.color = Color.black;
                break;
            case (glasswareState.THICK_POWDER):
                _meshRend.material.color = Color.green;
                break;
            case (glasswareState.ACID_DILUTED):
                _meshRend.material.color = new Color(1,1,0.6f);
                break;
            case (glasswareState.HEATED_ACID_STARCH_DILUTED):
                _meshRend.material.color = new Color(0.5f, 0.25f, 0);
                break;
            case (glasswareState.WATER):
                _onBecameWater.Invoke();
                _meshRend.material.color = new Color(0,0.2f,1);
                break;
            case (glasswareState.RABIES_VIRUS):
                _meshRend.material.color = new Color(0.6f, 0, 0);
                break;
            case glasswareState.SODIUM_CHLORIDE:
                _meshRend.material.SetColor("_Color", new Color(0.004f, 0.596f, 0.459f));
                break;
            case (glasswareState.POWDER):
                _meshRend.material.color = Color.white;
                break;
            case glasswareState.VALANCE:
                _meshRend.material.color = new Color(1, 0.28f, 0.3f);
                break;
            case glasswareState.DILUTED_SODIUM_CHLORIDE:
                _meshRend.material.color = new Color(0.72f, 0.9f, 0.76f);
                break;
            case glasswareState.HEATED_POWDER:
                _meshRend.material.color = Color.gray;
                break;
            case glasswareState.RABIES_VACCINE:
                _meshRend.material.color = new Color(0.64f, 0.28f, 0.34f);
                break;
        }
    }
}