using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooterObj : LevelObject
{
    [SerializeField] protected LaserManager lasermanager;
    private void Awake()
    {
        lasermanager = gameObject.transform.parent.parent.GetComponent<LaserManager>();
    }

}