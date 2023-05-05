using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;


public class Projectile : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip firingSound;
    [SerializeField] float damageAmount;
    void Awake() {

        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        PlayFiringSound();
    }

    public void PlayFiringSound() {
        float pitch = Random.Range(1, 1.1f);
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(firingSound, 0.5f);
    }

    public void Die(float projectileLifeTime) {
        Destroy(this.gameObject, projectileLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // Deal Damage to enemy
        if(collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent)) {
            enemyComponent.TakeDamage(damageAmount);
        }

        Destroy(gameObject);
    }

 
}
