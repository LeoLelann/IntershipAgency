using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
    

public class AdjustVolume : MonoBehaviour
{
    private LiftGammaGain gamma;
    private Vignette vignette;
    private Volume volume;
    // Start is called before the first frame update
    private void Start()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out gamma);
        volume.profile.TryGet(out vignette);
    }
    public void AdjustGamma(float value)
    {
        gamma.gamma.value = new Vector4(1f, 1f, 1f, value);

    }
    public void AdjustVignette(Vector2 pos)
    {
        StartCoroutine(ItNeededSmoothing(pos));
    }

    IEnumerator ItNeededSmoothing(Vector2 pos)
    {
        float timer = 0;
        Vector2 oldPos = (Vector2)vignette.center;
        float oldIntens = (float)vignette.intensity;
        while (timer < 2)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
            vignette.center.Override(Vector2.Lerp(oldPos, pos, timer / 2));
            vignette.intensity.Override(Mathf.Lerp(oldIntens, 1, timer));
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
