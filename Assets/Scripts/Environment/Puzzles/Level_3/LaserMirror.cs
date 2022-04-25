using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMirror : LaserShooterObj
{
    [SerializeField] private int start_direction;
    [SerializeField] private int direction;
    public Material material;
    [SerializeField] private bool _interactable;
    [SerializeField] private bool upDown;
    LaserBeam beam;
    Vector2 dir = new Vector2();

    private void Start()
    {
        if(_interactable)
        {
            RotateObject(start_direction);
        }
    }
    public override void Action()
    {
        if (_interactable)
        {
            Destroy(GameObject.Find("Laser Beam " + gameObject.name));
            direction++;
            direction %= 2;
            RotateObject(direction);
        }
        lasermanager.UpdateLaser();
    }

    public void ShootLaser()
    {
        if (upDown)
        {
            switch (direction)
            {
                case 1:
                    dir = new Vector2(0, -1); //down
                    break;
                case 0:
                    dir = new Vector2(0, 1); //up
                    break;
                
            }
        }
        else
        {
            switch (direction)
            {
                case 1:
                    dir = new Vector2(-1, 0); //left
                    break;
                case 0:
                    dir = new Vector2(1, 0); //right
                    break;
            }
        }
        Destroy(GameObject.Find("Laser Beam " + gameObject.name));
        beam = new LaserBeam(gameObject.transform.position, dir, material, gameObject, gameObject.name);
    }
    public override void ObjectReset()
    {
        Destroy(GameObject.Find("Laser Beam " + gameObject.name));
        if(_interactable)
        {
            RotateObject(start_direction);
        }
        
    }

    void RotateObject(int direction)
    {
        if(upDown)
        {
            gameObject.transform.eulerAngles = new Vector3(0, 0, (direction+1) * 90);
        }
        else
        {
            gameObject.transform.eulerAngles = new Vector3(0, 0, direction * 90);
        }
    }
}
