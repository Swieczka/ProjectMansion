using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public bool stay;
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
        SceneManager.LoadScene(num);
    }
}
