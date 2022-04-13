using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowObject : LevelObject
{
    public override void Action()
    {
        gameObject.SetActive(true);
    }
}
