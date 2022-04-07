using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : Interactable
{
    [SerializeField] private bool _OnGameLoadStay;
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
        if(!_OnGameLoadStay)
        {
            gameObject.SetActive(false);
        }
        
    }
}