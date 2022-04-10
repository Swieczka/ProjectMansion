using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaActivation : LevelObject
{
    [SerializeField] GameObject _gameObjecttoInteract;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _gameObjecttoInteract.GetComponent<LevelObject>().Action();
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    public override void ObjectReset()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
    }
}
