using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : Interactable
{
    void Update()
    {

    }

    public override void Action()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public override void ObjectReset()
    {
        gameObject.SetActive(false);
    }
}