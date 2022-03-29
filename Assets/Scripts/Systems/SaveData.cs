using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int CurrentBiom; 
    public float[] playerSpawnPos;
    public float[] camPos;
    public bool[] StoryCollectibles;

    public SaveData(GameManager manager)
    {
        CurrentBiom = manager.CurrentBiom;

        playerSpawnPos = new float[3];
        playerSpawnPos[0] = manager._player_respawn_position.x;
        playerSpawnPos[1] = manager._player_respawn_position.y;
        playerSpawnPos[2] = manager._player_respawn_position.z;

        camPos = new float[3];
        camPos[0] = manager._camera_x;
        camPos[1] = manager._camera_y;
        camPos[2] = -10;

        StoryCollectibles = new bool[manager.StoryCollectibles.Count];
        for(int i = 0; i < manager.StoryCollectibles.Count; i++)
        {
            StoryCollectibles[i] = manager.StoryCollectibles[i];
        }
    }
}
