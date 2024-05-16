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



    public override void Interacted(GameObject player)
    {
        if (player.transform.childCount > 1 && player.GetComponentInChildren<Glassware>().glasswareSt == Glassware.glasswareState.EMPTY)
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
