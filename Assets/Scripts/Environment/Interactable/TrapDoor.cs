using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : LevelObject
{
    [SerializeField] bool _isTrapDoorOpen;
    public List<Sprite> _sprites;
    private void Start()
    {
        _isTrapDoorOpen = false;
    }
    public override void Action()
    {
        _isTrapDoorOpen = !_isTrapDoorOpen;
        if(_isTrapDoorOpen)
        {
            GetComponent<SpriteRenderer>().sprite = _sprites[1];
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = _sprites[0];
            gameObject.GetComponent<Collider2D>().enabled = true;
        }
    }

    public override void ObjectReset()
    {
        GetComponent<SpriteRenderer>().sprite = _sprites[0];
        gameObject.GetComponent<Collider2D>().enabled = true;
    }
}
