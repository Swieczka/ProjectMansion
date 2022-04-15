using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMirror : LevelObject
{
    public int direction;
    public Material material;
    LaserBeam beam;
    Vector2 dir = new Vector2();
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
        Destroy(GameObject.Find("Laser Beam " + gameObject.name));
        direction++;
        direction %= 4;
    }

    public void ShootLaser()
    {
        switch (direction)
        {
            case 0:
                dir = new Vector2(0, 1);
                break;
            case 1:
                dir = new Vector2(1, 0);
                break;
            case 2:
                dir = new Vector2(0, -1);
                break;
            case 3:
                dir = new Vector2(-1, 0);
                break;
        }
        Destroy(GameObject.Find("Laser Beam " + gameObject.name));
        beam = new LaserBeam(gameObject.transform.position, dir, material, gameObject, gameObject.name);
    }
    public override void ObjectReset()
    {
        Destroy(GameObject.Find("Laser Beam " + gameObject.name));
    }
}
