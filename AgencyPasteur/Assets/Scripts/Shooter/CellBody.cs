using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBody : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Player>() != null&&GetComponentInParent<Cell>().PlayersIn==false)
        {
            GetComponentInParent<Cell>().PlayersIn = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            GetComponentInParent<Cell>().PlayersIn = false;
        }
    }
}
