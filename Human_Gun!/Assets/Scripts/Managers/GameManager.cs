using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Panels")] [SerializeField] private GameObject _startPanel;

    [SerializeField] private GameObject _nextLevelPanel;
    [SerializeField] private GameObject _finishPanel;


    private void Awake()
    {
        Time.timeScale = 0;
    }

    private void OnEnable()
    {
        EventManager.Instance.LoseGame += GameOver;
        EventManager.Instance.FinishGame += FinishGame;
    }

    private void OnDisable()
    {
        EventManager.Instance.LoseGame -= GameOver;
        EventManager.Instance.FinishGame -= FinishGame;
    }

    public void PlayGame()
    {
        _startPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void NextGame()
    {
        EventManager.Instance.OnLoadNextLevel();
    }

    public void RestartGame()
    {
        EventManager.Instance.OnLoadCurrentLevel();
    }

    public void FinishGame()
    {
        _nextLevelPanel.SetActive(true);
    }

    public void GameOver()
    {
        _finishPanel.SetActive(true);
    }
}