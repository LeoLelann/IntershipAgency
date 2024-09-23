using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AddToBook : MonoBehaviour
{
    [SerializeField] UnityEvent _newPage;

    [SerializeField] Glassware.glasswareState _glasswareState;
    [SerializeField] float _duration;
    [SerializeField] float _fullPageDuration;
    [SerializeField] Book _book;
    [SerializeField] Camera _cam;
    [SerializeField] GameObject _cover;

    public Glassware.glasswareState GlasswareState { get => _glasswareState; }

    public void Start()
    {
        Vector3 bookScreenPos = _cam.WorldToScreenPoint(_book.transform.position);
        _book.New();
       StartCoroutine(MoveToward(bookScreenPos));
    }

    IEnumerator MoveToward (Vector3 bookScreenPos)
    {
        _newPage.Invoke();
        yield return new WaitForSeconds(_fullPageDuration);
        float timer = 0;
        _book.LockedPage[GlasswareState].GetComponent<Page>().IsLocked = false;
        _book.LockedPage[GlasswareState].GetComponent<Page>().IsNew = true;
        Vector3 coverBasePos = _cover.transform.position;
        Vector3 coverBaseScale = _cover.transform.localScale;
        while (timer < _duration)
        {
            timer += Time.deltaTime;
            _cover.transform.position = Vector3.Lerp(coverBasePos, bookScreenPos, timer / _duration);
            _cover.transform.localScale = Vector3.Lerp(coverBaseScale, new Vector3(0.1f,0.1f,0.1f), timer / _duration);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        gameObject.SetActive(false);
        _cover.SetActive(false);
        _cover.transform.localPosition = new Vector3(0.5f,0.5f,0.5f);
        _cover.transform.localScale = new Vector3(1.2f,1.2f,1.2f);
    }
}
