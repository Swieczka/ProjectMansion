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
            PlateContent.SetActive(true);
        }
        else
        {
            PlateContent.SetActive(false);
        }
    }
}
