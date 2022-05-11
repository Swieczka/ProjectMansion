using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObject : LevelObject
{
    public override void Action()
    {
        gameObject.SetActive(false);
    }
}