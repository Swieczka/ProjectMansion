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
            GetComponent<Animator>().SetBool("isCharging", true);
            Move();
        }
        else
        {
            GetComponent<Animator>().SetBool("isCharging", false);
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
          //  states = RydwanStates.sleeping;
            collision.collider.gameObject.SetActive(false);
            StartCoroutine(RydwanDies());
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
            gameObject.transform.localScale = new Vector3(-2, 2, 1);
            GetComponent<Rigidbody2D>().velocity = Vector2.left * moveSpeed;
        }
        else
        {
            gameObject.transform.localScale = new Vector3(2, 2, 1);
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

    private IEnumerator RydwanDies()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        GetComponent<RydwanAudio>().RydwanDies();
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
