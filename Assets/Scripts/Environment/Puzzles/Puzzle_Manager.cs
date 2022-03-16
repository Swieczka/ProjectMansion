using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_Manager : MonoBehaviour
{
    void Start()
    {
        Check();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ResetPuzzles();
        }
    }
    public virtual void Check()
    {

    }

    public virtual void ResetPuzzles()
    {

    }

}
