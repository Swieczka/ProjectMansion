using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactable
{
    [SerializeField] Sprite[] LeverSprites;
    [SerializeField] bool _isLeverActive;
    [SerializeField] GameObject _gameObjecttoInteract;
    public override void Action()
    {
        _isLeverActive = !_isLeverActive;
        _gameObjecttoInteract.GetComponent<LevelObject>().Action();
        if(_isLeverActive)
        {
            GetComponent<SpriteRenderer>().sprite = LeverSprites[0];
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = LeverSprites[1];
        }
    }

    public override void ObjectReset()
    {
        _isLeverActive =false;
        GetComponent<SpriteRenderer>().sprite = LeverSprites[1];
    }
}
