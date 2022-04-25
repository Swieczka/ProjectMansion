using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam
{
    Vector2 pos, dir;

    public GameObject laserObj;
    LineRenderer laser;
    List<Vector2> laserIndices = new List<Vector2>();
    List<GameObject> objects = new List<GameObject>();
    public LaserBeam(Vector2 pos, Vector2 dir, Material material, GameObject parent, string name)
    {
        this.laser = new LineRenderer();
        this.laserObj = new GameObject();
        this.laserObj.name = "Laser Beam "+name;
        this.laserObj.tag = "Laser";
        this.laserObj.transform.parent = parent.transform;
        this.pos = pos;
        this.dir = dir;

        this.laser= this.laserObj.AddComponent<LineRenderer>();
        this.laser.startWidth = 0.1f;
        this.laser.endWidth = 0.1f;
        this.laser.material = material;
        this.laser.startColor = Color.yellow;

         CastRay(pos,dir,laser,parent); 
    }

    void CastRay(Vector2 pos, Vector2 dir, LineRenderer laser,GameObject parent)
    {
        laserIndices.Add(pos);

        Ray2D ray = new Ray2D(pos, dir);
        RaycastHit2D hit=Physics2D.Raycast(pos+dir,dir,20f,LayerMask.GetMask("Laser"));
        Debug.Log(hit.collider.gameObject.name);
        if (hit.collider.gameObject != null && hit.collider.gameObject != parent)
         {
            laserIndices.Add(hit.point);
            UpdateLaser();
            if(hit.collider.gameObject.GetComponent<LaserMirror>() != null)
            {
                hit.collider.gameObject.GetComponent<LaserMirror>().ShootLaser();
            }
            else if(hit.collider.gameObject.GetComponent<LaserEndPoint>() != null)
            {
                hit.collider.gameObject.GetComponent<LaserEndPoint>().Action();
            }
            else if(hit.collider.gameObject.GetComponent<LaserDuplicator>() != null)
            {
                hit.collider.gameObject.GetComponent<LaserDuplicator>().Action();
            }
        }
        else
        {
             laserIndices.Add(ray.GetPoint(10));
             UpdateLaser();
        }
    }

    void UpdateLaser()
    {
        int counter = 0;
        laser.positionCount = laserIndices.Count;

        foreach(Vector2 index in laserIndices)
        {
            laser.SetPosition(counter,index);
            counter++;
        }
    }
}
