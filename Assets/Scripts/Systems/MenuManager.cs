using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public bool stay;
    [System.Serializable]
    public class StartBiomsPositions
    {
        public int BiomIndex;
        public float _camera_x;
        public float _camera_y;
        public Vector3 _player_respawn_position;
    }
    public static MenuManager Instance;
    public List<StartBiomsPositions> _startBiomsPositions;
    private void Awake()
    {
        if(stay)
        {
            gameObject.SetActive(false);
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
            DontDestroyOnLoad(gameObject);
            
        }
        
    }
    
    public void StartNewGame()
    {
        GameManager.instance.RestartStats();
        SaveSystem.SaveGame(GameManager.instance);
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        SaveData CheckBiom = SaveSystem.LoadGame();
        int x = CheckBiom.CurrentBiom;
        SceneManager.LoadScene(x);
    }

    public void BackToMenu()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

    public void Hide(GameObject x)
    {
        Time.timeScale = 1.0f;
        x.SetActive(false);
    }

    public void GoToScene(int num)
    {
        StartBiomsPositions biom = _startBiomsPositions[num];
        GameManager.instance.NextBiom(biom.BiomIndex, biom._camera_x, biom._camera_y, biom._player_respawn_position);
    }
    public void GoToScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void ShowCanvas(GameObject canvas)
    {
        canvas.SetActive(true);
    }
    public void HideCanvas(GameObject canvas)
    {
        canvas.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeBrightness()
    {
        PlayerPrefs.SetFloat("Light",GetComponent<Slider>().value);
        GameManager.instance.LightIntensity();
    }

    public void ChangeVolume()
    {
        PlayerPrefs.SetFloat("Sound", GetComponent<Slider>().value);
        GameManager.instance.SoundVolume(GetComponent<Slider>().value);
    }

    public void ChangeSFX()
    {
        PlayerPrefs.SetFloat("SFX", GetComponent<Slider>().value);
        GameManager.instance.SFXVolume(GetComponent<Slider>().value);
    }
}
