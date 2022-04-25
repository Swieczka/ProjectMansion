using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEndPoint : LevelObject
{
    public int index;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public override void Action()
    {
        Debug.Log("Laser activated: "+index.ToString());
    }

    public override void ObjectReset()
    {
        base.ObjectReset();
    }
}
