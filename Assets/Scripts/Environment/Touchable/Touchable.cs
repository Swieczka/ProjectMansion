using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touchable : LevelObject
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            Action();
        }
    }
}
