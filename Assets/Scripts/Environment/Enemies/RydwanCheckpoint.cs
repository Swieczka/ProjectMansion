using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RydwanCheckpoint : MonoBehaviour
{
    public GameObject[] RydwanWalls;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach(GameObject wall in RydwanWalls)
        {
            wall.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach( GameObject wall in RydwanWalls)
        {
            wall.SetActive(false);
        }
    }
}
