using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy enemyToSpawn;
    [SerializeField] float timeBetweenSpawns;
    [SerializeField] float maxSpawnOffset;
    EnemyWaveSO enemyWave;

    private void Start()
    {
        enemyWave = GameManager.Instance.GetActiveWave();
        Spawn();
    }

    public void Spawn() {
        enemyWave = GameManager.Instance.GetActiveWave();
        StartCoroutine(SpawnEnemies());
    }


    IEnumerator SpawnEnemies()
    {

        for(int i = 0; i < enemyWave.enemyMax; i++)
        {
            Vector2 spawnPos = transform.position + new Vector3(0,Random.Range(0,maxSpawnOffset));
            Instantiate(enemyToSpawn, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenSpawns);

        }
    }
}
