using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    public float cooldown;
    public float cooldownBase;
    private void Start()
    {
        
        cooldownBase = 4;
        cooldown = Time.time-cooldownBase;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && Time.time > cooldown + cooldownBase)
        {
            Debug.Log(audioSource.clip.name + " is playing");
            cooldown = Time.time;
            audioSource.Play();
        }
    }
}
