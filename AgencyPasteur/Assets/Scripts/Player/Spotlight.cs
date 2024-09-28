using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spotlight : MonoBehaviour
{
    private static Spotlight instance;
    [SerializeField] private Camera _cam;
    [SerializeField] private AdjustVolume _renderVolume;
    public static Spotlight Instance => instance;

    private void InitSingleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }
    private void Awake()
    {
        InitSingleton();
    }


    public void SpotLighted(string elementName, float duration)
    {
        GameObject target = GameObject.Find(elementName);
        Vector3 targetCamPos = _cam.WorldToScreenPoint(target.transform.position);
        Debug.LogWarning(targetCamPos);
        Vector2 ratioPos = new Vector2(targetCamPos.x / Camera.main.pixelWidth, targetCamPos.y / Camera.main.pixelHeight);
        Debug.LogWarning(ratioPos);
        StartCoroutine(TargetSpotlight(ratioPos, duration));
    }
    IEnumerator TargetSpotlight(Vector2 v, float duration)
    {
        _renderVolume.AdjustGamma(-0.1f);
        _renderVolume.AdjustVignette(v);
        yield return new WaitForSeconds(duration);
        _renderVolume.StopVignette();
        yield return null;

    }
}
