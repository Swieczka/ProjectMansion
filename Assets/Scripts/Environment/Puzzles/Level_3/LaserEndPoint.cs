using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEndPoint : LaserShooterObj
{
    public bool _has_correct_state = false;
    public GameObject lightObj;

    public override void Action()
    {
        _has_correct_state = true;
        lightObj.SetActive(true);
    }

    public override void ObjectReset()
    {
        _has_correct_state =false;
        lightObj.SetActive(false);
    }
}
