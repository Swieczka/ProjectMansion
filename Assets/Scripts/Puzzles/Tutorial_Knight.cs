using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Knight : MonoBehaviour
{
    [SerializeField] private int knight_state;
    [SerializeField] private int knight_correct_state;
    [SerializeField] public bool knight_has_correct_state;
    [SerializeField] bool is_player_nearby;
    [SerializeField] private Sprite[] KnightSprites = new Sprite[4];
    [SerializeField] private Tutorial_Knight_Manager _Manager;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        _Manager = GetComponentInParent<Tutorial_Knight_Manager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        knight_state = 0;
        spriteRenderer.sprite = KnightSprites[0];
    }
    private void Update()
    {
        if (is_player_nearby)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ChangeKnightState();
                _Manager.CheckKnights();
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

    private void ChangeKnightState()
    {
        knight_state++;
        int state = knight_state % 4;
        switch (state)
        {
            case 0:
                spriteRenderer.sprite = KnightSprites[0];
                break;
            case 1:
                spriteRenderer.sprite = KnightSprites[1];
                break;
            case 2:
                spriteRenderer.sprite = KnightSprites[2];
                break;
            case 3:
                spriteRenderer.sprite = KnightSprites[3];
                break;
        }
        knight_state = state;
        if (knight_state == knight_correct_state)
        {
            knight_has_correct_state = true;
        }
        else
        {
            knight_has_correct_state = false;
        }
    }
}
