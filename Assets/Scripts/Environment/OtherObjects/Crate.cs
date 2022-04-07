using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : LevelObject
{
    [SerializeField] Vector3 cratePos;
    public override void ObjectReset()
    {
        gameObject.transform.position = cratePos;
    }

}
