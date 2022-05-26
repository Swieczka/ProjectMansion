using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;

    public static GameManager instance;
    public int CurrentBiom;
    public float _camera_x;
    public float _camera_y;
    public Vector3 _player_respawn_position;

    public List<bool> StoryCollectibles = new List<bool>();
    public Light2D lightScreen;
    string[] cheatCode = new string[] {"j","p", "2", "2", "1", "3", "7"};
    int cheatIndex = 0;

    public AudioMixer mixer;
    enum gameMode { normal,pope};
    gameMode GameMode = gameMode.normal;
    Image img;
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
        img = GetComponentInChildren<Image>();
        GameMode = gameMode.normal;
        Scene currScene = SceneManager.GetActiveScene();
        if (currScene.buildIndex != 0)
        {
            LoadGame();
        }
        else
        {
            LoadInMenu();
        }
        LightIntensity();
    }
    void Update()
    {
        if(Input.anyKeyDown)
        {
            if (Input.GetKeyDown(cheatCode[cheatIndex]))
            {
                cheatIndex++;
                if(cheatIndex == cheatCode.Length)
                {
                    GameMode = gameMode.pope;
                    cheatIndex = 0;
                }
            }
            else
            {
                cheatIndex = 0;
            }
        }
        switch (GameMode)
        {
            case gameMode.normal:
                if (GetComponent<AudioSource>() != null)
                {
                    Destroy(GetComponent<AudioSource>());
                }
                lightScreen.color = Color.white;
                lightScreen.intensity = PlayerPrefs.GetFloat("Light");
                img.sprite = null;
                img.color = new Color(0, 0, 0, 0);
                img.gameObject.SetActive(false);
                break;
            case gameMode.pope:
                img.sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Environment/OtherObjects/image.jpg", typeof(Sprite));
                img.color = new Color(1, 1, 1, 0.2f);
                img.gameObject.SetActive(true);
                AudioListener.volume = 2;
                lightScreen.color = Color.yellow;
                lightScreen.intensity = 1;
                if (GetComponent<AudioSource>() == null)
                {
                    AudioSource source = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
                    source.loop = true;
                    source.clip = (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Environment/OtherObjects/sound.mp3", typeof(AudioClip));
                    source.Play();
                }
                break;
        }
       
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadGame();
        }
        if(Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 9 && SceneManager.GetActiveScene().buildIndex != 11)
        {
            if(pauseMenu.activeInHierarchy)
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1.0f;
            }
            else
            {
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
            }
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

       /* for (int i = 0; i < StoryCollectibles.Count; i++)
        {
            StoryCollectibles[i] = data.StoryCollectibles[i];
        }*/
    }
    public void LoadGame()
    {
        GameMode = gameMode.normal;
        ResetObjects();
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
        GameMode = gameMode.normal;
    }
    public void NextBiom(int biom, float x, float y, Vector3 res)
    {
        CurrentBiom = biom;
        _camera_x = x;
        _camera_y = y;
        _player_respawn_position = res;
        SaveGame();
        GameMode = gameMode.normal;
        SceneManager.LoadScene(CurrentBiom);
    }

    public void ResetObjects()
    {
        GameMode = gameMode.normal;
        LevelObject[] levelObjects = Resources.FindObjectsOfTypeAll<LevelObject>();
        foreach(LevelObject levelObject in levelObjects)
        {
            levelObject.ObjectReset();
        }
    }

    public void LockMovement(bool _lock)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>()._MoveRes = !_lock;
        Debug.Log(!_lock);
    }

    public void LightIntensity()
    {
        lightScreen.intensity = PlayerPrefs.GetFloat("Light");
    }

    public void SoundVolume(float num)
    {
        mixer.SetFloat("MusicVol",num);
    }

    public void SFXVolume(float num)
    {
        mixer.SetFloat("SFXVol", num);
    }
}
