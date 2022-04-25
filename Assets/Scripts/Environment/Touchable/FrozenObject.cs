using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenObject : LevelObject
{
    public bool _canMove;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Spikes")
        {
            if (_canMove)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.right * 1.5f;
            }
        }
    }

    public override void Action()
    {
        _canMove = true;
    }

    public override void ObjectReset()
    {
        _canMove = false;
    }
}
