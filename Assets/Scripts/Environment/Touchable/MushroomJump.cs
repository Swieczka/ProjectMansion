using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomJump : Touchable
{
    public float _jumpForce;
    public override void Action()
    {
        Rigidbody2D _rb = playerObj.GetComponent<Rigidbody2D>();
        _rb.velocity = new Vector2(_rb.velocity.x, 0f);
        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        playerObj.GetComponent<Animator>().Play("Base Layer.Jump Start");
    }
}
