using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event Action<Enemy> OnEnemyKilled;
    public static event Action<Enemy> OnEnemyAttack;
    [SerializeField] float movementSpeed;
    [SerializeField] float health, maxHealth = 3f;
    [SerializeField] int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(movementSpeed * Time.deltaTime , 0, 0);
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
        GameManager.Instance.TakeDamage(damage);
    }

}
