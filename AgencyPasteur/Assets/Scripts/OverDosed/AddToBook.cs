using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToBook : MonoBehaviour
{
    [SerializeField] Glassware.glasswareState _glasswareState;
    [SerializeField] float _duration;
    [SerializeField] Book _book;
    [SerializeField] Camera _cam;
    [SerializeField] GameObject _cover;

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
            _cover.transform.position = Vector3.Lerp(_cover.transform.position, bookScreenPos, timer / _duration);
            _cover.transform.localScale = Vector3.Lerp(_cover.transform.localScale, new Vector3(0.1f,0.1f,0.1f), timer / _duration);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        _book.LockedPage[GlasswareState].GetComponent<Page>().IsLocked = false;
        gameObject.SetActive(false);
        _cover.SetActive(false);
        _cover.transform.localPosition = new Vector3(0.5f,0.5f,0.5f);
        _cover.transform.localScale = new Vector3(1.2f,1.2f,1.2f);
        Debug.Log("&hh");
    }
}
