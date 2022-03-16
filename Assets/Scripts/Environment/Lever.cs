using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactable
{
    [SerializeField] Sprite[] LeverSprites;
    [SerializeField] bool _isLeverActive;
    protected override void Action()
    {
        _isLeverActive = !_isLeverActive;
        if(_isLeverActive)
        {
            GetComponent<SpriteRenderer>().sprite = LeverSprites[0];
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = LeverSprites[1];
        }
    }
}
