using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI enemiesLeftText;
    List<Enemy> enemies = new List<Enemy>();

    void Awake()
    {
        enemies = new List<Enemy>();
        enemies = GameObject.FindObjectsOfType<Enemy>().ToList();
        UpdateEnemiesLeftText();
    }

    private void OnEnable() {
        Enemy.OnEnemyKilled += HandleEnemyDefeated;

    }
    private void OnDisable() {
        Enemy.OnEnemyKilled -= HandleEnemyDefeated;
    }

    void Start()
    {
        Debug.Log(enemies.Count);
    }

    void UpdateEnemiesLeftText()
    {
        enemiesLeftText.text = $"Enemies left: {enemies.Count}";
    }

    void HandleEnemyDefeated(Enemy enemy)
    {
        if (enemies.Remove(enemy))
        {
            UpdateEnemiesLeftText();
        }
    }

}
