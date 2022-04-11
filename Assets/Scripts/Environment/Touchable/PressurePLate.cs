using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePLate : LevelObject
{
    public LevelObject _gameObjectToInterract;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.GetComponent<LevelObject>() != null)
        {
            Action();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.GetComponent<LevelObject>() != null)
        {
            Action();
        }
    }

    public override void Action()
    {
        _gameObjectToInterract.Action();
    }
}
