using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartNewGame()
    {
        GameManager.instance.RestartStats();
        SaveSystem.SaveGame(GameManager.instance);
        SceneManager.LoadScene(4);
    }

    public void LoadGame()
    {
        SaveData CheckBiom = SaveSystem.LoadGame();
        int x = CheckBiom.CurrentBiom;
        SceneManager.LoadScene(x);
    }
}
