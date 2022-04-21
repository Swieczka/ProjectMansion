using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    float cooldown;
    float cooldownBase;
    private void Start()
    {
        cooldown = Time.time;
        cooldownBase = 4;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && Time.time > cooldown + cooldownBase)
        {
            cooldown = Time.time;
            audioSource.Play();
        }
    }
}
