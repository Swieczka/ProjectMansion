using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curatin : MonoBehaviour
{
    public GameObject curtain;
    private void Start()
    {
        curtain = GameObject.Find("Curtain");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (curtain != null)
        {
            StartCoroutine(CurtainInOut());
        }
    }


    private IEnumerator CurtainInOut()
    {
        curtain.GetComponent<Animator>().SetFloat("Multiplier", 2.2f);
        curtain.GetComponent<Animator>().Play("Base Layer.FadeIn");
        yield return new WaitForSeconds(3);
        curtain.GetComponent<Animator>().SetFloat("Multiplier", 1f);
        curtain.GetComponent<Animator>().Play("Base Layer.FadeOut");
    }
}
