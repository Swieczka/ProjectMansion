using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerState : MonoBehaviour
{
    public PlayerMovement player;
    public bool playerResAfterCollide;
    public enum playerRestrictions
    {
        move,
        jump,
        d_jump,
        dash
    }
    public playerRestrictions _playerRestrictions;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player = collision.gameObject.GetComponent<PlayerMovement>();
            switch(_playerRestrictions)
            {
                case playerRestrictions.move:
                    player._MoveRes = playerResAfterCollide;
                    break;
                case playerRestrictions.jump:
                    player._JumpRes = playerResAfterCollide;
                    break;
                case playerRestrictions.d_jump:
                    player._DoubleJumpRes = playerResAfterCollide;
                    break;
                case playerRestrictions.dash:
                    player._DashRes = playerResAfterCollide;
                    break;
            }

        }
    }
}
