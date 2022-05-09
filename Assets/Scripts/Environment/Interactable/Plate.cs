using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : Interactable
{
    [SerializeField] GameObject PlateContent;
    void Start()
    {
        is_player_nearby = false;
        PlateContent.SetActive(false);
    }
    public override void Action()
    {
        if (!PlateContent.activeSelf)
        {
            GameManager.instance.LockMovement(true);
            PlateContent.SetActive(true);
        }
        else
        {
            GameManager.instance.LockMovement(false);
            PlateContent.SetActive(false);
        }
    }
}
