using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddToBookTuto : MonoBehaviour
{
    [SerializeField] float _duration;
    [SerializeField] Image _image;
    // Start is called before the first frame update
    void OnEnable()
    {

        StartCoroutine(PageFadeIn());
    }
    IEnumerator PageFadeIn()
    {
        float timer = 0;
        while (timer < _duration)
        {
            timer += Time.deltaTime;
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, Mathf.Lerp(0, 1, timer / _duration));
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    
}
