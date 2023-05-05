using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    void OnEnable() {
        GameManager.OnPlayerDeath += HandlePlayerDeath;
    }
    void OnDisable() {
        GameManager.OnPlayerDeath -= HandlePlayerDeath;
    }

    private void HandlePlayerDeath()
    {
        ShowGameOverPanel();
    }
    private void HideGameOverPanel()
    {
        gameOverPanel.SetActive(false);
    }

    public void LoadGame(){
        SceneManager.LoadScene("Game");
    }
    public void QuitGame(){
        Application.Quit();
    }
    private void ShowGameOverPanel(){
        gameOverPanel.SetActive(true);
    }
}
