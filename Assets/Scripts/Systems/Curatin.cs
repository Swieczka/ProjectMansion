using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curatin : MonoBehaviour
{
    public bool In;
    public GameObject curtain;
    private void Start()
    {
        curtain = GameObject.Find("Curtain");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (curtain != null)
        {
            if (In)
            {
                curtain.GetComponent<Animator>().Play("Base Layer.FadeIn");
            }
            else
            {
                Debug.Log("asd");
                curtain.GetComponent<Animator>().Play("Base Layer.FadeOut");
            }
        }
    }
}
