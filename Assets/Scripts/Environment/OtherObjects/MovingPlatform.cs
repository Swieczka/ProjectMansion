using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    Vector3 leftBorder;
    Vector3 rightBorder;
    public int facingLeft;
    public float moveSpeed;
    public float range;
    void Start()
    {
        facingLeft = Random.Range(0, 2) == 0 ? -1 : 1;
        leftBorder = new Vector3(transform.position.x - range, transform.position.y, transform.position.z);
        rightBorder = new Vector3(transform.position.x + range, transform.position.y, transform.position.z);
    }

    void Update()
    {
        Move();
    }
    void Move()
    {
        if (transform.position.x <= leftBorder.x)
        {
            facingLeft = -1;
        }
        if (transform.position.x >= rightBorder.x)
        {
            facingLeft = 1;
        }

        if (facingLeft == -1)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.right * moveSpeed;
        }
        else if (facingLeft == 1)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.left * moveSpeed;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            GameObject player = collision.collider.gameObject;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x,player.GetComponent<Rigidbody2D>().velocity.y);
        }
    }
}
