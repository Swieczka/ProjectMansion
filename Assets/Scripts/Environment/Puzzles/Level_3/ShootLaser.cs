using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    public Material material;
    LaserBeam beam;

    private void Start()
    {
        LaserShoot();
    }
    public void LaserShoot()
    {
        Destroy(GameObject.Find("Laser Beam " + gameObject.name));
        beam = new LaserBeam(gameObject.transform.position, transform.up * -1, material, gameObject, gameObject.name);
    }
}
