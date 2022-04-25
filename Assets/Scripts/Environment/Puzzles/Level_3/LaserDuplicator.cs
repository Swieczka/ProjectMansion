using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDuplicator : LevelObject
{
    public Material material;
    LaserBeam beam1;
    LaserBeam beam2;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ObjectReset();
        }
    }
    public override void Action()
    {
        beam1 = new LaserBeam(gameObject.transform.position, Vector2.right, material, gameObject, gameObject.name);
        beam2 = new LaserBeam(gameObject.transform.position, Vector2.right*-1, material, gameObject, gameObject.name);
    }

    public override void ObjectReset()
    {
        Destroy(GameObject.Find("Laser Beam " + gameObject.name));
        Destroy(GameObject.Find("Laser Beam " + gameObject.name));
    }

}
