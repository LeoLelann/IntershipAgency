using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerElement : Interactable
{
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
            Debug.Log("feur2");
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
            }
            else
            {
                //feedback négatif pour faire comprendre le manque de verrerie ou verrerie pleine
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
                    m.material.color = Color.white;
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
            collision.transform.parent = transform;
            collision.transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
            collision.transform.rotation = new Quaternion(-90, 0, 0, 0);
            collision.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        }
    }
}
