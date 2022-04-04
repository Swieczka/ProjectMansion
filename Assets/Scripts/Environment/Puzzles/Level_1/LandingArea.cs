using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingArea : MonoBehaviour
{
    public int areaIndex;
    public Vector3 areaPos;
    private void Start()
    {
        areaPos.x = transform.position.x;
        areaPos.y = transform.position.y;
        areaPos.z = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Puzzle>() != null)
        {
            collision.gameObject.transform.position = areaPos;
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            collision.gameObject.GetComponent<Puzzle>()._state = areaIndex;
            collision.gameObject.GetComponent<Puzzle>().Action();
        }
    }
}
