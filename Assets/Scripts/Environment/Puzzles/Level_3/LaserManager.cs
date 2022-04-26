using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : Puzzle_Manager
{
    [SerializeField] LevelObject[] laserObjects;
    [SerializeField] LaserEndPoint[] laserEndPoints;
    [SerializeField] ShootLaser shootlaser;
    [SerializeField] GameObject IceZone;
    [SerializeField] GameObject FrozenObj;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ResetPuzzles();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            UpdateLaser();
        }
    }

    public override void Check()
    {
        int check_value=0;
        foreach(var laserendpoint in laserEndPoints)
        {
            if(laserendpoint._has_correct_state)
            {
                check_value++;
            }
        }
        if(check_value == laserEndPoints.Length)
        {
            IceZone.SetActive(false);
            FrozenObj.GetComponent<LevelObject>().Action();
        }
        else
        {
            IceZone.SetActive(true);
        }
    }
    public void UpdateLaser()
    {
        foreach (var laserendpoint in laserEndPoints)
        {
            laserendpoint.ObjectReset();
        }
        StartCoroutine(waitandshoot(0.1f));
    }
    public override void ResetPuzzles()
    {
        IceZone.SetActive(true);
        foreach (var laserobj in laserObjects)
        {
            laserobj.ObjectReset();
        }
        shootlaser.LaserShoot();

    }

    IEnumerator waitandshoot(float time)
    {
        GameObject[] children = GameObject.FindGameObjectsWithTag("Laser");
        foreach (GameObject child in children)
        {
            Destroy(child);
        }
        yield return new WaitForSeconds(time);
        shootlaser.LaserShoot();
        Check();
    }

}
