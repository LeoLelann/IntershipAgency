using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToBook : MonoBehaviour
{
    [SerializeField] Glassware.glasswareState _glasswareState;
    [SerializeField] float _duration;
    [SerializeField] Book _book;
    [SerializeField] Camera _cam;

    public Glassware.glasswareState GlasswareState { get => _glasswareState; }

    public void Start()
    {
        Vector3 bookScreenPos = _cam.WorldToScreenPoint(_book.transform.position);
       StartCoroutine(MoveToward(bookScreenPos));
    }

    IEnumerator MoveToward (Vector3 bookScreenPos)
    {
        yield return new WaitForSeconds(2);
        float timer = 0;
        while (timer < _duration)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
            transform.position = Vector3.Lerp(transform.position, bookScreenPos, timer / _duration);
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.1f,0.1f,0.1f), timer / _duration);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;
        _book.LockedPage[GlasswareState].GetComponent<Page>().IsLocked = false;
        gameObject.SetActive(false);
        Debug.Log("&hh");
    }
}
