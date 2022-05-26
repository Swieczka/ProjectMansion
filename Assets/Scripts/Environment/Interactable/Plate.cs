using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : Interactable
{
    public bool plateActive = false;
    [SerializeField] GameObject PlateContent;
    void Start()
    {
        is_player_nearby = false;
        PlateContent.SetActive(false);
    }
    public override void Action()
    {
        if (!plateActive)
        {
            
            GameManager.instance.LockMovement(true);
            PlateContent.SetActive(true);
            plateActive = true;
        }
        else
        {
            GameManager.instance.LockMovement(false);
            PlateContent.SetActive(false);
            plateActive = false;
        }
    }
}
