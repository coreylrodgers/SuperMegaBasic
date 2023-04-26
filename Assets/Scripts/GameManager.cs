using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] TextMeshProUGUI enemiesLeftText;
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
    }

    private void Start()
    {
        activeWave = enemyWaveList.list[0];

        enemiesLeft = activeWave.enemyMax;
        UpdateEnemiesLeftText();
    }

    private void Update()
    {
        if (enemiesLeft <= 0 && waveIndex != (enemyWaveList.list.Count - 1))
        {
            waveIndex++;
            SetActiveWave(enemyWaveList.list[waveIndex]);
            enemiesLeft = activeWave.enemyMax;
            enemySpawner.Spawn();
            UpdateEnemiesLeftText();

        }
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
