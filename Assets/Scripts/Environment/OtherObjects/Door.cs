using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : LevelObject
{
    [SerializeField] bool isOpen=false;
    public override void Action()
    {
        Debug.Log("drzwi");
        isOpen = !isOpen;
        if(isOpen)
        {
            GetComponent<Animator>().SetFloat("AnimSpeed", 1f);
            GetComponent<Animator>().Play("Base Layer.Open");
        }
        else
        {
            GetComponent<Animator>().SetFloat("AnimSpeed", -1f);
            GetComponent<Animator>().Play("Base Layer.Open");
        }
    }
}
