using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RydwanAudio : MonoBehaviour
{
    public AudioClip musicSleeping;
    public AudioClip musicCharging;
    private AudioSource audioPlayer;
    private Rydwan rydwan;

    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        rydwan = GetComponent<Rydwan>();
    }

    private void Update()
    {
        if (rydwan.states == Rydwan.RydwanStates.sleeping)
        {
            audioPlayer.clip = musicSleeping;
        }
        else
        {
            audioPlayer.clip = musicCharging;
        }
        if (!audioPlayer.isPlaying) audioPlayer.Play();
    }
}
