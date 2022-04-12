using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int CurrentBiom;
    public float _camera_x;
    public float _camera_y;
    public Vector3 _player_respawn_position;

    public List<bool> StoryCollectibles = new List<bool>();
    private void Awake()
    {
        
        if(instance != null)
        {
           Destroy(gameObject);
        }
        else
        {
            instance = this; 
        }
        DontDestroyOnLoad(gameObject);
        
        
        
    }
    void Start()
    {
        Scene currScene = SceneManager.GetActiveScene();
        if (currScene.buildIndex != 0)
        {
            LoadGame();
        }
        else
        {
            LoadInMenu();
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F5))
        {
            SaveGame();
        }
        if (Input.GetKeyDown(KeyCode.F9))
        {
            LoadGame();
        }
    }

    public void DebugText(string text)
    {
        Debug.Log(text);
    }
    public void ChangeCameraPos(float x, float y)
    {
        _camera_x = x;
        _camera_y = y;
        GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(_camera_x, _camera_y, -10f);
    }

    public void ChangePlayerRespawn(Vector3 pos)
    {
        _player_respawn_position = pos;
    }
    public void LoadInMenu()
    {
        SaveData data = SaveSystem.LoadGame();
        CurrentBiom = data.CurrentBiom;
        Vector3 pos;
        pos.x = data.playerSpawnPos[0];
        pos.y = data.playerSpawnPos[1];
        pos.z = data.playerSpawnPos[2];
        _player_respawn_position = pos;

        _camera_x = data.camPos[0];
        _camera_y = data.camPos[1];

        for (int i = 0; i < StoryCollectibles.Count; i++)
        {
            StoryCollectibles[i] = data.StoryCollectibles[i];
        }
    }
    public void LoadGame()
    {
      //  ResetObjects();
        SaveData data = SaveSystem.LoadGame();
        CurrentBiom = data.CurrentBiom;
        Vector3 pos;
        pos.x = data.playerSpawnPos[0];
        pos.y = data.playerSpawnPos[1];
        pos.z = data.playerSpawnPos[2];
        _player_respawn_position = pos;

        _camera_x = data.camPos[0];
        _camera_y = data.camPos[1];
        ChangeCameraPos(_camera_x, _camera_y);
        GameObject.FindGameObjectWithTag("Player").transform.position = _player_respawn_position;
       /* for (int i = 0; i < StoryCollectibles.Count; i++)
        {
            StoryCollectibles[i] = data.StoryCollectibles[i];
        } */
        
    }
    public void SaveGame()
    {
        SaveSystem.SaveGame(this);
    }

    public void RestartStats()
    {
        _camera_x = 0;
        _camera_y = 0;
        CurrentBiom = 4;
        _player_respawn_position = new Vector3(-14, 5, 0);
        for(int x=0; x < StoryCollectibles.Count; x++)
        {
            StoryCollectibles[x] = false;
        }
    }
    public void NextBiom(int biom, float x, float y, Vector3 res)
    {
        CurrentBiom = biom;
        _camera_x = x;
        _camera_y = y;
        _player_respawn_position = res;
        SaveGame();
        SceneManager.LoadScene(CurrentBiom);
    }

    public void ResetObjects()
    {
        LevelObject[] levelObjects = Resources.FindObjectsOfTypeAll<LevelObject>();
        foreach(LevelObject levelObject in levelObjects)
        {
            levelObject.ObjectReset();
        }
    }
}
