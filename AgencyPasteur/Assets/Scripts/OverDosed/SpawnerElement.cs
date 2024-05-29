using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnerElement : Interactable
{
    public UnityEvent OnNoGlasswareToTakeElement;
    public UnityEvent OnElementTaken; 
    public UnityEvent OnTakeFrom;
    public UnityEvent OnSnapGlassware;
    public enum Elements
    {
        TALC,
        ACID,
        STARCH
    };
    [SerializeField] private Elements element;

    private void Start()
    {
        OnStateValueChange(element);
    }

    public override void Interacted(GameObject player)
    {

        if (transform.GetComponentInChildren<Glassware>()!=null && player.transform.GetComponentInChildren<Glassware>()==null)
        {
            OnTakeFrom?.Invoke();
            transform.GetComponentInChildren<Glassware>().Interacted(player);
        }
        else
        {
            if (player.transform.GetComponentInChildren<Glassware>()!=null && player.GetComponentInChildren<Glassware>().GlasswareSt == Glassware.glasswareState.EMPTY)
            {
                switch (element)
                {
                    case Elements.TALC:
                        player.GetComponentInChildren<Glassware>().SetGlasswareState(Glassware.glasswareState.TALC);
                        break;
                    case Elements.ACID:
                        player.GetComponentInChildren<Glassware>().SetGlasswareState(Glassware.glasswareState.ACID);
                        break;
                    case Elements.STARCH:
                        player.GetComponentInChildren<Glassware>().SetGlasswareState(Glassware.glasswareState.STARCH);
                        break;
                }
                OnElementTaken?.Invoke();
            }
            else
            {
                OnNoGlasswareToTakeElement?.Invoke();
            }
        }
        
    }
    private void OnStateValueChange(Elements state)
    {
        switch (state)
        {
            case (Elements.ACID):
                MeshRenderer[] mesh = GetComponentsInChildren<MeshRenderer>();
                foreach(MeshRenderer m in mesh)
                {
                    m.material.color = Color.yellow;
                }
                break;
            case (Elements.STARCH):
                MeshRenderer[] mesh2 = GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer m in mesh2)
                {
                    m.material.color = new Color(1,0.75f,0.8f);
                }
                break;
            case (Elements.TALC):
                MeshRenderer[] mesh3 = GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer m in mesh3)
                {
                    m.material.color = Color.blue;
                }
                break;
         }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.CompareTag("Glassware") && collision.transform.parent == null && transform.GetComponentInChildren<Glassware>() == null)
        {
            OnSnapGlassware?.Invoke();
            collision.transform.parent = transform;
            collision.transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
            collision.transform.rotation = new Quaternion(-90, 0, 0, 0);
            collision.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        }
    }
}
