using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour
{
    [SerializeField] private float cd_start;
    [SerializeField] private float cd_add;
    [SerializeField] private GameObject cable;
    void Start()
    {
        cd_start = Time.time;
    }

    void Update()
    {
        if(Time.time > cd_start + cd_add)
        {
            cd_start = Time.time;
            if(cable.activeSelf)
            {
                cable.SetActive(false);
            }
            else
            {
                cable.SetActive(true);
            }
        }
    }
}
