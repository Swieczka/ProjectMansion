using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    public Material material;
    LaserBeam beam;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Destroy(GameObject.Find("Laser Beam " + gameObject.name));
            beam = new LaserBeam(gameObject.transform.position, transform.right, material, gameObject, gameObject.name);

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Destroy(GameObject.Find("Laser Beam " + gameObject.name));
        }
    }
}
