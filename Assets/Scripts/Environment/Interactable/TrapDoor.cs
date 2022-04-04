using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : LevelObject
{
    [SerializeField] bool _isTrapDoorOpen;
    private void Start()
    {
        _isTrapDoorOpen = false;
    }
    public override void Action()
    {
        _isTrapDoorOpen = !_isTrapDoorOpen;
        if(_isTrapDoorOpen)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0.3f);
        }
        else
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
            gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        }
    }

    public override void ObjectReset()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.black;
    }
}
