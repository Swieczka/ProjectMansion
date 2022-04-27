using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemies : MonoBehaviour
{
    void Start()
    {
        Jump();  
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Ground")
        {
            Jump();
        }
    }
}
