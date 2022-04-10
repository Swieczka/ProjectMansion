using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling_Platform : LevelObject
{
    [SerializeField] Vector3 position;
    [SerializeField] float gravitySpeed;
    public override void Action()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Rigidbody2D>().gravityScale = gravitySpeed;
    }

    public override void ObjectReset()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        gameObject.transform.position = position;
    }
}
