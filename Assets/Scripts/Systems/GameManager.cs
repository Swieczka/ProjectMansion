using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float _camera_x;
    public float _camera_y;
    public float _player_x;
    public float _player_y;
    public Vector3 _player_respawn_position;
    public int deathcount;
    private void Awake()
    {
        deathcount = 0;
        
        if(instance != null)
        {
           Destroy(gameObject);
        }
        else
        {
            instance = this; 
        }
        DontDestroyOnLoad(gameObject);
        StartSetup();
    }
    void Start()
    {
        if (deathcount == 0)
        {
            PlayerPrefs.SetFloat("PlayerX", -14f);
            PlayerPrefs.SetFloat("PlayerY", 5f);
            ChangeCameraPos(0, 0);
        }
        
    }

    void Update()
    {
        
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
        _player_x = pos.x;
        _player_y = pos.y;
        PlayerPrefs.SetFloat("PlayerX", _player_x);
        PlayerPrefs.SetFloat("PlayerY", _player_y);
    }

    public void RestartLevel()
    {
        deathcount++;
        PlayerPrefs.SetFloat("CamX",_camera_x);
        PlayerPrefs.SetFloat("CamY", _camera_y);
        PlayerPrefs.SetFloat("PlayerX", _player_x);
        PlayerPrefs.SetFloat("PlayerY", _player_y);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartSetup()
    {
        _camera_x = PlayerPrefs.GetFloat("CamX");
        _camera_y = PlayerPrefs.GetFloat("CamY");
        _player_x = PlayerPrefs.GetFloat("PlayerX");
        _player_y = PlayerPrefs.GetFloat("PlayerY");
        _player_respawn_position = new Vector3(_player_x, _player_y, 0f);
        ChangeCameraPos(_camera_x, _camera_y);
        GameObject.FindGameObjectWithTag("Player").transform.position = _player_respawn_position;
    }
}
