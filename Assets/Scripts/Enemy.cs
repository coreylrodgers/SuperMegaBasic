using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event Action<Enemy> OnEnemyKilled;
    [SerializeField] float movementSpeed;
    [SerializeField] float health, maxHealth = 3f;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(movementSpeed * Time.deltaTime , 0, 0);
    
        
    }
    public void TakeDamage(float damageAmount) {
        health -= damageAmount;
        if(health <= 0) {
            Destroy(gameObject);
            OnEnemyKilled?.Invoke(this);
        }

    }

}
