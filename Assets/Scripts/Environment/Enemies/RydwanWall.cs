using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RydwanWall : MonoBehaviour
{
    public GameObject wall;
    private void Start()
    {
        if (wall != null)
        {
            wall.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name =="Rydwan")
        {
            if (wall != null)
            {
                wall.SetActive(true);
            }
            
        }
    }
}
