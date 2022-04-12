using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] private float _cam_x;
    [SerializeField] private float _cam_y;
    [SerializeField] private Vector3 _player_pos;
    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            gameManager.ChangeCameraPos(_cam_x, _cam_y);
            gameManager.ChangePlayerRespawn(_player_pos);
            gameManager.SaveGame();
        }
        
    }
}
