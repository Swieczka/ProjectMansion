using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDuplicator : LaserShooterObj
{
    public Material material;
    LaserBeam beam1;
    LaserBeam beam2;
    public override void Action()
    {
        beam1 = new LaserBeam(gameObject.transform.position, Vector2.right, material, gameObject, gameObject.name);
        beam2 = new LaserBeam(gameObject.transform.position, Vector2.right*-1, material, gameObject, gameObject.name);
        
    }

    public override void ObjectReset()
    {
        GameObject[] children = GameObject.FindGameObjectsWithTag("Laser");
        foreach(GameObject child in children)
        {
            Destroy(child);
        }
    }

}
