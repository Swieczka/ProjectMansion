using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] protected bool is_player_nearby;

    void Update()
    {
        if (is_player_nearby && Input.GetKeyDown(KeyCode.E))
        {
            Action();
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
    protected virtual void Action()
    {

    }
}
