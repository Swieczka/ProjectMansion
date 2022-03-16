using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    [SerializeField] protected bool is_player_nearby;
    [SerializeField] protected int _state;
    [SerializeField] protected int _correct_state;
    [SerializeField] public bool _has_correct_state;

    void Update()
    {
        if (is_player_nearby && Input.GetKeyDown(KeyCode.E))
        {
            PuzzleAction();
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
    protected virtual void PuzzleAction()
    {

    }
    protected virtual void PuzzleCheck()
    {

    }
    public virtual void ResetPuzzle()
    {

    }
}
