using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWall : LaserShooterObj
{
    int rot;
    private void Start()
    {
        rot = 0;
    }
    public override void Action()
    {
        rot++;
        rot %= 2;
        switch(rot)
        {
            case 0:
                gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
                break;
            case 1:
                gameObject.transform.eulerAngles = new Vector3(0, 0, 90);
                break;
        }
        lasermanager.UpdateLaser();
    }

    public override void ObjectReset()
    {
        gameObject.transform.eulerAngles = new Vector3(0, 0, 90);
    }
}
