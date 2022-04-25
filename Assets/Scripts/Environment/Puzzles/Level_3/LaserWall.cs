using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWall : LevelObject
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ObjectReset();
        }
    }
    public override void Action()
    {
        gameObject.transform.eulerAngles = new Vector3(0, 0, gameObject.transform.rotation.z + 90);
    }

    public override void ObjectReset()
    {
        gameObject.transform.eulerAngles = new Vector3(0, 0, 90);
    }
}
