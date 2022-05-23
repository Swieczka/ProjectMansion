using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RydwanAudio : MonoBehaviour
{
    public AudioClip musicSleeping;
    public AudioClip musicCharging;
    [SerializeField] private AudioSource audioPlayer;
    private Rydwan rydwan;

    private void Start()
    {
        rydwan = GetComponent<Rydwan>();
    }

    public void RydwanDies()
    {
        audioPlayer.clip = musicSleeping;
        if (!audioPlayer.isPlaying) audioPlayer.Play();
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
