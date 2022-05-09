using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public List<StartBiomsPositions> _startBiomsPositions;
    private void Awake()
    {
        if(stay)
        {
            DontDestroyOnLoad(gameObject);
            gameObject.SetActive(false);
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
        canvas.SetActive(!canvas.activeSelf);
    }
}
