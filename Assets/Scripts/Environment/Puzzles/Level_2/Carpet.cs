using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carpet : LevelObject
{
    [SerializeField] Vector3 startPos;
    [SerializeField] Vector3 spawnPos;
    [SerializeField] Vector3 endPos;
    bool IsMoving = false;
    private void Awake()
    {
        spawnPos = endPos;
    }
    public override void Action()
    {
        Vector3 tempPos = startPos;
        startPos = endPos;
        endPos = tempPos;
        IsMoving = true;
    }

    private void Update()
    {
        if (IsMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, Time.deltaTime * 1f);
        }
        if (transform.position == endPos)
        {
            IsMoving = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.GetComponent<Crate>() != null)
        {
            collision.collider.gameObject.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.GetComponent<Crate>() != null)
        {
            collision.collider.gameObject.transform.parent = transform.parent.transform.parent;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(startPos, endPos);
    }

    public override void ObjectReset()
    {
       // gameObject.transform.position = spawnPos;
    }
}
