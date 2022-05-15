using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllusionShaderProximity : MonoBehaviour
{
    private Material material;
    [SerializeField][Range(0, 1)] private float distortion;
    [SerializeField][Range(0, 100)] private float distortionScale;

    private void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        material.SetFloat("Distortion", distortion);
        material.SetFloat("DistortionScale", distortionScale);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(IllusionFadeAway());
    }

    private IEnumerator IllusionFadeAway()
    {
        while (material.GetFloat("Distortion") < 1)
        {
           material.SetFloat("Distortion", material.GetFloat("Distortion") + Time.deltaTime * 0.1f);
        }
        while (material.GetFloat("DistortionScale") > 0)
        {
            material.SetFloat("DistortionScale", material.GetFloat("DistortionScale") - Time.deltaTime * 0.1f);
        }
        yield return null;
    }
}
