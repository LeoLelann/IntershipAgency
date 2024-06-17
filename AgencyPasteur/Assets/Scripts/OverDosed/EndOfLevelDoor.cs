using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

public class EndOfLevelDoor : MonoBehaviour
{

   [SerializeField] private GameObject _doorRotate;
   [SerializeField] private UnityEvent _onOpen;
    [SerializeField] private GameObject _UI_cinematique;
    [SerializeField] private Cinematique _cine1;
    int _areTheyGone;
    public void OnEnd()
    {
        StartCoroutine(RotateDoor());
    }
    IEnumerator RotateDoor()
    {
        float timer=0;
        _onOpen.Invoke();
        while (timer < 2)
        {
            timer += Time.deltaTime;
            _doorRotate.transform.eulerAngles = new Vector3(90, 0, 180-45*timer);

            Debug.Log(timer);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        foreach(UILookAtCamera i in FindObjectsOfType<UILookAtCamera>())
        {
            i.gameObject.SetActive(false);
        }
        GetComponent<Collider>().isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _areTheyGone++;
            Destroy(other.gameObject);
        }
        if (_areTheyGone >= 3)
        {
            _UI_cinematique.SetActive(true);
            _cine1.NextSlide();
        }
    }
}
