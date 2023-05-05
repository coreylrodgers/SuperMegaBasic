using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event Action<Enemy> OnEnemyKilled;
    public static event Action<Enemy> OnEnemyAttack;

    [SerializeField] EnemySO enemySO;
    float health;
    void Start()
    {
        health = enemySO.health;
    }

    void Update()
    {
        transform.position += new Vector3(enemySO.speed * Time.deltaTime, 0, 0);
        if(transform.position.x > 10) {
            DealDamage();
            OnEnemyAttack?.Invoke(this);
            Destroy(gameObject);
        }
    }
    public void TakeDamage(float damageAmount) {
        health -= damageAmount;
        if(health <= 0) {
            Destroy(gameObject);
            OnEnemyKilled?.Invoke(this);
        }
    }
    public void DealDamage() {
        GameManager.Instance.TakeDamage(enemySO.damage);
    }

}
