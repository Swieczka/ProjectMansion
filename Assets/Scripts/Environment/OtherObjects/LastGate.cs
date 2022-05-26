using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastGate : LevelObject
{
    public Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Action()
    {
        GetComponent<Rigidbody2D>().gravityScale = 2f;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        GetComponent<Rigidbody2D>().freezeRotation = true;
    }

    public override void ObjectReset()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        transform.position = startPos;
    }
}
