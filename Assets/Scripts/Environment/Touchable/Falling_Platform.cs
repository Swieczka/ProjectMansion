using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling_Platform : LevelObject
{
    [SerializeField] Vector3 position;
    [SerializeField] float gravitySpeed;
    private void Start()
    {
        position = transform.position;
        if (GetComponent<Animator>() != null)
        {
            GetComponent<Animator>().Play("Base Layer.Idle");
        }
    }
    public override void Action()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Rigidbody2D>().gravityScale = gravitySpeed;
        if(GetComponent<Animator>() != null)
        {
            GetComponent<Animator>().Play("Base Layer.Falling");
        }
    }

    public override void ObjectReset()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        gameObject.transform.position = position;
        if (GetComponent<Animator>() != null)
        {
            GetComponent<Animator>().Play("Base Layer.Idle");
        }
        foreach(AreaActivation area in gameObject.GetComponentsInChildren<AreaActivation>())
        {
            area.ObjectReset();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player" || collision.collider.gameObject.layer == 8)
        {
            StartCoroutine(Wait(2));
            ObjectReset();
        }
    }

    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
