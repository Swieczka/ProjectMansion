using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderArea : LevelObject
{
    public override void Action()
    {
        GetComponent<Collider2D>().enabled = !GetComponent<Collider2D>().enabled;
    }
}
