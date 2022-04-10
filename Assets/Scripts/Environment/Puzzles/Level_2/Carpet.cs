using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carpet : LevelObject
{
    [SerializeField] Vector3 startPos;
    [SerializeField] Vector3 endPos;
    public override void Action()
    {
        transform.position = Vector3.Lerp(startPos, endPos, Time.deltaTime);
        Vector3 tempPos = startPos;
        startPos = endPos;
        endPos = tempPos;
    }
}
