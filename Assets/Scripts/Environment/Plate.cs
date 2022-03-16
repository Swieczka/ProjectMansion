using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField] bool is_player_nearby;
    [SerializeField] GameObject PlateContent;
    void Start()
    {
        is_player_nearby = false;
        PlateContent.SetActive(false);
    }

    void Update()
    {
        if (is_player_nearby && Input.GetKeyDown(KeyCode.E))
        {
            if(!PlateContent.activeSelf)
            {
                PlateContent.SetActive(true);
            }
            else
            {
                PlateContent.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            is_player_nearby = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            is_player_nearby = false;
        }
    }
}
