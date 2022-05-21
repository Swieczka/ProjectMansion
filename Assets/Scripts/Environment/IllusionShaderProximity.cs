using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllusionShaderProximity : MonoBehaviour
{
    private Material material;
    private MeshRenderer meshRenderer;
    [SerializeField][Range(0, 1)] private float distortion;
    [SerializeField][Range(0, 100)] private float distortionScale;
    [SerializeField][Range(0, 2)] private float changeSpeedModificator;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer.material;
        material.SetFloat("Distortion", distortion);
        material.SetFloat("DistortionScale", distortionScale);
        material.SetFloat("Opacity", 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StopAllCoroutines();
        StartCoroutine(IllusionFadeAway());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StopAllCoroutines();
        StartCoroutine(IllusionFadeIn());
    }

    private IEnumerator IllusionFadeAway()
    {
        meshRenderer.enabled = true;
        float t = 0f;

        while (t < 1)
        {
            material.SetFloat("Distortion", Mathf.Lerp(material.GetFloat("Distortion"), 1, t));
            material.SetFloat("DistortionScale", Mathf.Lerp(material.GetFloat("DistortionScale"), 0, t));
            material.SetFloat("Opacity", Mathf.Lerp(material.GetFloat("Opacity"), 0, t));
            t += Time.deltaTime * changeSpeedModificator;
            if (t >= 1) meshRenderer.enabled = false;
            yield return null;
        }
       // meshRenderer.enabled = false;

        /*while (material.GetFloat("Distortion") < 1)
        {
           material.SetFloat("Distortion", material.GetFloat("Distortion") + Time.deltaTime);
            yield return null;
        }
        while (material.GetFloat("DistortionScale") > 0)
        {
            material.SetFloat("DistortionScale", material.GetFloat("DistortionScale") - Time.deltaTime * 10f);
            yield return null;
        }*/

    }

    private IEnumerator IllusionFadeIn()
    {
        meshRenderer.enabled = true;
        float t = 0f;

        while (t < 1)
        {
            material.SetFloat("Distortion", Mathf.Lerp(material.GetFloat("Distortion"), distortion, t));
            material.SetFloat("DistortionScale", Mathf.Lerp(material.GetFloat("DistortionScale"), distortionScale, t));
            material.SetFloat("Opacity", Mathf.Lerp(material.GetFloat("Opacity"), 1, t));
            t += Time.deltaTime * changeSpeedModificator;
            yield return null;
        }

        /*while (material.GetFloat("Distortion") > distortion)
        {
            material.SetFloat("Distortion", material.GetFloat("Distortion") - Time.deltaTime);
            yield return null;
        }
        while (material.GetFloat("DistortionScale") < distortionScale)
        {
            material.SetFloat("DistortionScale", material.GetFloat("DistortionScale") + Time.deltaTime * 10f);
            yield return null;
        }*/
    }
}
