using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collapsing_Platform : Touchable
{
    Collider2D collider;
    public override void Action()
    {
        collider = gameObject.GetComponent<Collider2D>();
        StartCoroutine(DestroyPlatform());
    }
    public override void ObjectReset()
    {
        StopAllCoroutines();
        collider = gameObject.GetComponent<Collider2D>();
        collider.enabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Animator>().Play("Base Layer.Idle");
    }
    private IEnumerator DestroyPlatform()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<Animator>().Play("Base Layer.Disappear");
        yield return new WaitForSeconds(0.4f);
        collider.enabled = false;
        //gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}
