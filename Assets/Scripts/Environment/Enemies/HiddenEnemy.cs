using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenEnemy : LevelObject
{
    [SerializeField] bool Up;

    public override void Action()
    {
        if (Up)
        {
            GetComponent<Animator>().Play("Base Layer.Up");
        }
        else
        {
            GetComponent<Animator>().Play("Base Layer.Down");
        }
    }
}
