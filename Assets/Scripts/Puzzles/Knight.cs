using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    [SerializeField] private int knight_state;
    [SerializeField] private int knight_correct_state;
    [SerializeField] bool knight_has_correct_state;
    [SerializeField] bool is_player_nearby;
    void Start()
    {
        knight_state = 0;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    private void Update()
    {
        if(is_player_nearby)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ChangeKnightState();
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
            is_player_nearby=false;
        }
    }

    private void ChangeKnightState()
    {
        knight_state++;
        int state = knight_state % 4;
        switch(state)
        {
            case 0:
                GetComponent<SpriteRenderer>().color = Color.white;
                break;
            case 1:
                GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case 2:
                GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case 3:
                GetComponent<SpriteRenderer>().color = Color.blue;
                break;
        }
        knight_state = state;
        if(knight_state == knight_correct_state)
        {
            knight_has_correct_state = true;
        }
        else
        {
            knight_has_correct_state = false;
        }
    }
}
