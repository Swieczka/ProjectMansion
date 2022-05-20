using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touchable : LevelObject
{
    protected GameObject playerObj;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            playerObj = collision.collider.gameObject;
            Action();
        }
    }
}
