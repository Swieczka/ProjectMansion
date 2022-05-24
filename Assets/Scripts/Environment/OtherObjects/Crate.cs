using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : LevelObject
{
    [SerializeField] Vector3 cratePos;
    public bool not_resetable;
    public override void ObjectReset()
    {
        if (!not_resetable)
        {
            gameObject.transform.position = cratePos;
        }
    }

}
