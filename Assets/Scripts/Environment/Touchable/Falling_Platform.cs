using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling_Platform : LevelObject
{
    [SerializeField] Vector3 position;
    [SerializeField] float gravitySpeed;
    private void Start()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        position = transform.position;
        if (GetComponent<Animator>() != null)
        {
            GetComponent<Animator>().Play("Base Layer.Idle");
        }
    }
    public override void Action()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Rigidbody2D>().freezeRotation = true;
        GetComponent<Rigidbody2D>().gravityScale = gravitySpeed;
        GetComponent<BoxCollider2D>().enabled = true;
        if (GetComponent<Animator>() != null)
        {
            GetComponent<Animator>().Play("Base Layer.Falling");
        }
    }

    public override void ObjectReset()
    {
        StartCoroutine(Wait(3));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player" || collision.collider.gameObject.layer == 8)
        {
            ObjectReset();  
        }
    }

    private IEnumerator Wait(float time)
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        gameObject.transform.position = position;
        if (GetComponent<Animator>() != null)
        {
            GetComponent<Animator>().Play("Base Layer.Idle");
        }

        yield return new WaitForSeconds(time);
        foreach (AreaActivation area in gameObject.GetComponentsInChildren<AreaActivation>())
        {
            area.ObjectReset();
        }
    }
}
