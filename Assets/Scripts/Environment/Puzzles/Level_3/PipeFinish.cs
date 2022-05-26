using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeFinish : LevelObject
{
    bool isOpen;
    public Transform tpToTransform;
    public Collider2D WallCollider;

    public void Action(bool open)
    {
        WallCollider.enabled = !open;
        GetComponent<SpriteRenderer>().enabled = !open;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.SetPositionAndRotation(tpToTransform.position, tpToTransform.rotation);
        }
    }

    public override void ObjectReset()
    {
        WallCollider.enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
    }
}
