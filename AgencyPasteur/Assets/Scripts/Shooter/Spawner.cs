using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour//UNUSED
{    
    public void Spawn(GameObject enemy)
    {
        Instantiate(enemy, transform);
    }
}
