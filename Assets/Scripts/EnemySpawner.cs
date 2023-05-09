using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemySO enemyToSpawn;
    [SerializeField] float timeBetweenSpawns;
    [SerializeField] float maxSpawnOffset;
    EnemyWaveSO enemyWave;
    private void OnEnable()
    {
        Enemy.OnEnemyAttack += HandleEnemyAttack;
    }
    private void OnDisable()
    {
        Enemy.OnEnemyAttack -= HandleEnemyAttack;
    }

    void HandleEnemyAttack(Enemy enemy) {
        SpawnEnemy(enemy);
    }
    
    private void Start()
    {
        enemyWave = GameManager.Instance.GetActiveWave();
        Spawn();
    }

    public void Spawn() {
        enemyWave = GameManager.Instance.GetActiveWave();
        StartCoroutine(SpawnEnemies());
    }

    void OnDrawGizmos() {
        Debug.DrawLine(transform.position, transform.position + new Vector3(0, maxSpawnOffset, 0));
        Debug.DrawLine(transform.position, transform.position + new Vector3(0, -maxSpawnOffset, 0));
    }

    public void SpawnEnemy(Enemy newEnemyToSpawn) {
        Vector2 spawnPos = transform.position + new Vector3(0,Random.Range(-maxSpawnOffset,maxSpawnOffset));
        Instantiate(newEnemyToSpawn, spawnPos, Quaternion.identity);
    }


    IEnumerator SpawnEnemies()
    {
        for(int i = 0; i < enemyWave.enemyMax; i++)
        {
            Vector2 spawnPos = transform.position + new Vector3(0,Random.Range(0,maxSpawnOffset));
            Instantiate(enemyToSpawn.pf, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenSpawns);

        }
    }
}
