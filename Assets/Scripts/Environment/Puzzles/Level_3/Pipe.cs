using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : Puzzle
{
    [SerializeField] private int alt_correct_state;
    [SerializeField] private int start_state;
    void Start()
    {
        gameObject.transform.eulerAngles = new Vector3(0, 0, start_state*-90);
        _Manager = GetComponentInParent<Puzzle_Manager>();
        _state = start_state;
        PuzzleCheck();
    }

    protected override void PuzzleCheck()
    {
        int state = _state % 4;
        _state = state;
        /*   switch (state)
           {
               case 0:
                   gameObject.transform.rotation = Quaternion.FromToRotation(transform.rotation.eulerAngles, new Vector3(0, 0, 0));
                   break;
               case 1:
                   gameObject.transform.rotation = Quaternion.FromToRotation(transform.rotation.eulerAngles, new Vector3(0, 0, -90));
                   break;
               case 2:
                   gameObject.transform.rotation = Quaternion.FromToRotation(transform.rotation.eulerAngles, new Vector3(0, 0, 180));
                   break;
               case 3:
                   gameObject.transform.rotation = Quaternion.FromToRotation(transform.rotation.eulerAngles, new Vector3(0, 0, 90));
                   break;
           }  */
        if (_state == _correct_state || _state == alt_correct_state)
        {
            _has_correct_state = true;
        }
        else
        {
            _has_correct_state = false;
        }
    }
    public override void Action()
    {
        gameObject.transform.eulerAngles = new Vector3(0, 0, transform.rotation.eulerAngles.z - 90);
        _state++;
        PuzzleCheck();
        _Manager.Check();
    }

    public override void ObjectReset()
    {
      //  _state = start_state;
      // gameObject.transform.eulerAngles = new Vector3(0, 0, start_state * -90);
      //  PuzzleCheck();
    }
}
