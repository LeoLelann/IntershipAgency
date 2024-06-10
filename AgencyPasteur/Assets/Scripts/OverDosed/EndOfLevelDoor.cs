using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevelDoor : MonoBehaviour
{
   [SerializeField] private GameObject _doorRotate;
   [SerializeField] private GameObject _doorKnob;
    int _areTheyGone;
    private void OnEnd()
    {
        StartCoroutine(RotateDoor());
    }
    IEnumerator RotateDoor()
    {
        float timer=0;
        while (timer < 2)
        {
            timer += Time.deltaTime;
            _doorRotate.transform.eulerAngles = new Vector3(0, 45*timer, 0);
            _doorKnob.transform.eulerAngles = new Vector3(0, 45*timer, 0);

            Debug.Log(timer);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        GetComponent<Collider>().isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _areTheyGone++;
            Destroy(other);
        }
        if (_areTheyGone >= 3)
        {
            // Change Scene;
        }
    }
}
