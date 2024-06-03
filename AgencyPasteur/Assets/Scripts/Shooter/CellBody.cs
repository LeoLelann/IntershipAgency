using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBody : MonoBehaviour//UNUSED
{
    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.name);
        if (other.GetComponent<PlayerShooter>() != null&&GetComponentInParent<Cell>().PlayersIn==false)
        {
            GetComponentInParent<Cell>().PlayersIn = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerShooter>() != null)
        {
            GetComponentInParent<Cell>().PlayersIn = false;
        }
    }
}
