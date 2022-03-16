using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Knight : Puzzle
{
    [SerializeField] private Sprite[] KnightSprites = new Sprite[4];
    [SerializeField] private Tutorial_Knight_Manager _Manager;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        _Manager = GetComponentInParent<Tutorial_Knight_Manager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _state = 0;
        PuzzleCheck();
    }
    private void ChangeKnightState()
    {
        _state++;
        PuzzleCheck();
    }
    public override void Action()
    {
        ChangeKnightState();
        _Manager.Check();
    }
    protected override void PuzzleCheck()
    {
        int state = _state % 4;
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
        _state = state;
        if (_state == _correct_state)
        {
            _has_correct_state = true;
        }
        else
        {
            _has_correct_state = false;
        }
    }
    public override void ResetPuzzle()
    {
        Debug.Log(gameObject.name + " Reseted");
        _state = 0;
        PuzzleCheck();
    }
}
