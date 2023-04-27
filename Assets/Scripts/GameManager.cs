using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    [SerializeField] int health;
    [SerializeField] bool gameOver;
    [SerializeField] int maxHealth = 5;

    [SerializeField] TextMeshProUGUI enemiesLeftText;
    [SerializeField] TextMeshProUGUI healthRemainingText;
    [SerializeField] EnemyWaveSO activeWave;
    [SerializeField] int enemiesLeft;
    [SerializeField] EnemyWaveListSO enemyWaveList;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] int waveIndex;

    private void Awake()
    {
        Instance = this;
        enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
        enemyWaveList = Resources.Load<EnemyWaveListSO>(typeof(EnemyWaveListSO).Name);
        if (enemyWaveList == null)
        {
            Debug.Log("Cant load enemy list");
        }
        health = maxHealth;
    }

    private void Start()
    {
        activeWave = enemyWaveList.list[0];
        enemiesLeft = activeWave.enemyMax;
        UpdateEnemiesLeftText();
        UpdateHealthLeftText();
    }

    private void Update()
    {
        
        if (!gameOver)
        {
            if (enemiesLeft <= 0 && waveIndex != (enemyWaveList.list.Count - 1))
            {
                waveIndex++;
                SetActiveWave(enemyWaveList.list[waveIndex]);
                enemiesLeft = activeWave.enemyMax;
                enemySpawner.Spawn();
                UpdateEnemiesLeftText();

            }
            if (health <= 0)
            {
                gameOver = true;
                GameOver();
            }
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        _SceneManager.Instance.ShowGameOverScene();
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        UpdateHealthLeftText();

    }
    public void SetActiveWave(EnemyWaveSO enemyWave)
    {
        activeWave = enemyWave;
    }

    public EnemyWaveSO GetActiveWave()
    {
        return activeWave;
    }

    private void OnEnable()
    {
        Enemy.OnEnemyKilled += HandleEnemyDefeated;

    }
    private void OnDisable()
    {
        Enemy.OnEnemyKilled -= HandleEnemyDefeated;
    }

    void UpdateEnemiesLeftText()
    {
        enemiesLeftText.text = $"Enemies left: {enemiesLeft}";
    }

    void UpdateHealthLeftText()
    {
        healthRemainingText.text = $"Health: {health}";
    }

    public int GetEnemiesLeft()
    {
        return enemiesLeft;
    }

    void HandleEnemyDefeated(Enemy enemy)
    {
        enemiesLeft -= 1;
        UpdateEnemiesLeftText();
    }

}
