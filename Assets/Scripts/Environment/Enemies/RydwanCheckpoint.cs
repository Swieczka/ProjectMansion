using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RydwanCheckpoint : MonoBehaviour
{
    public GameObject[] RydwanWallsActive;
    public GameObject[] RydwanWallsInactive;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (GameObject wall in RydwanWallsActive)
            {
                if (wall != null)
                {
                    wall.SetActive(true);
                }
            }
            foreach (GameObject wall in RydwanWallsInactive)
            {
                if (wall != null)
                {
                    wall.SetActive(false);
                }
            }
        }
    }
}
