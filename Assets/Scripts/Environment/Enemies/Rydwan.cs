using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rydwan : LevelObject
{

    public enum RydwanStates
    {
        sleeping,
        charging
    }
    public RydwanStates states;
    public Vector3 spawnPos;
    public bool goLeft;
    public float moveSpeed;
    void Start()
    {
        states = RydwanStates.sleeping;
    }

    void Update()
    {
        if(states == RydwanStates.charging)
        {
            Move();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "BreakRydwan")
        {
            collision.collider.GetComponent<Animator>().Play("Base Layer.Break");
            Collider2D[] colliders = collision.collider.GetComponents<Collider2D>();
            foreach(Collider2D collider2d in colliders)
            {
                collider2d.enabled = false;
            }
        }
        if(collision.collider.gameObject.name == "RydwanEndWall")
        {
            states = RydwanStates.sleeping;
            collision.collider.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().tag == "RydwanWall")
        {
            goLeft = !goLeft;
        }
    }
    private void Move()
    {
        if(goLeft)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.left * moveSpeed;
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.right * moveSpeed;
        }
    }

    public override void Action()
    {
        states = RydwanStates.charging;
    }

    public override void ObjectReset()
    {
        
    }
}
