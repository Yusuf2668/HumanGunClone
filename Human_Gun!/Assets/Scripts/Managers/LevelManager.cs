using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerPrefs.SetInt(nameof(StringType.PlayerPrefs.levelPrefs), 1);
        LoadSaveScene();
        EventManager.Instance.LoadCurrentLevel += RestartGame;
        EventManager.Instance.LoadNextLevel += LoadNextLevel;
    }

  

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void LoadSaveScene()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt(nameof(StringType.PlayerPrefs.levelPrefs), 1));
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadNextLevel()
    {
        var _currentBuildIndex = SceneManager.GetActiveScene().buildIndex;

        if (SceneManager.GetSceneByName("Level02").buildIndex == _currentBuildIndex)
        {
            PlayerPrefs.SetInt(nameof(StringType.PlayerPrefs.levelPrefs), 1);
            SceneManager.LoadScene(1);
        }
        else
        {
            PlayerPrefs.SetInt(nameof(StringType.PlayerPrefs.levelPrefs), _currentBuildIndex + 1);
            SceneManager.LoadScene(_currentBuildIndex + 1);
        }
    }
}