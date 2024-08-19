using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cinematique : MonoBehaviour
{
    [SerializeField] Cinematique _nextImage;
   
    
    IEnumerator Appear()
    {
        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime;
            Image i = GetComponent<Image>();
            i.color = new Color(i.color.r, i.color.g, i.color.b, Mathf.Lerp(0, 1, timer / 1));
            yield return new WaitForSeconds(Time.deltaTime);
        }
        StartCoroutine(NextImage());
    }
    IEnumerator NextImage()
    {
        yield return new WaitForSeconds(2);
        if (_nextImage != null)
            _nextImage.NextSlide();
        else
            SceneManager.LoadScene("VieilleSceneMenu");

    }
    public void NextSlide()
    {
        StartCoroutine(Appear());
    }
    private void Start()
    {
        
    }
}
