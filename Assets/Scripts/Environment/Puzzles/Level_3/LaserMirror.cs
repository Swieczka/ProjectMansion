using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMirror : LevelObject
{
    [SerializeField] private int direction;
    public Material material;
    [SerializeField] private bool _interactable;
    [SerializeField] private bool upDown;
    [SerializeField] private bool all;
    LaserBeam beam;
    Vector2 dir = new Vector2();

    private void Start()
    {
        if(_interactable)
        {
            gameObject.transform.eulerAngles = new Vector3(0, 0, (direction - 1) * -90);
        }
        
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            Action();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            ObjectReset();
        }
    }
    public override void Action()
    {
        if (_interactable)
        {
            Destroy(GameObject.Find("Laser Beam " + gameObject.name));
            direction++;
            direction %= 2;
            gameObject.transform.eulerAngles = new Vector3(0, 0, (direction - 1) * -90);
        }
    }

    public void ShootLaser()
    {
        if (upDown)
        {
            switch (direction)
            {
                case 1:
                    dir = new Vector2(0, 1);
                    break;
                case 0:
                    dir = new Vector2(0, -1);    
                    break;
                
            }
        }
        else
        {
            switch (direction)
            {
                case 1:
                    dir = new Vector2(-1, 0);
                    break;
                case 0:
                    dir = new Vector2(1, 0);
                    break;
            }
        }
        Destroy(GameObject.Find("Laser Beam " + gameObject.name));
        beam = new LaserBeam(gameObject.transform.position, dir, material, gameObject, gameObject.name);
    }
    public override void ObjectReset()
    {
        Destroy(GameObject.Find("Laser Beam " + gameObject.name));
    }
}
