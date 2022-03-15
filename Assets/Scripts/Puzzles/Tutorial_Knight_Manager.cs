using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Knight_Manager : MonoBehaviour
{
    public Tutorial_Knight[] Knights = new Tutorial_Knight[3];
    void Start()
    {
        CheckKnights();
    } 
    public void CheckKnights()
    {
        int correct_counter = 0;
        foreach (var knight in Knights)
        {
            if (knight.knight_has_correct_state)
            {
                correct_counter++;
            }
            if (correct_counter == 3)
            {
                Debug.Log("Dobrze!");
            }
        }
    }
}
