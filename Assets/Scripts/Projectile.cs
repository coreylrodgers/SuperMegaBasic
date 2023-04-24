using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;


public class Projectile : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip firingSound;
    // Start is called before the first frame update
    void Awake() {

        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        PlayFiringSound();
    }

    // Update is called once per frame
    void Update()
    {
        // Travel 
    }
    
    public void PlayFiringSound() {
        //Set random pitch
        float pitch = Random.Range(1, 1.1f);
        audioSource.pitch = pitch;
        //play
        audioSource.PlayOneShot(firingSound, 0.5f);
    }

    public void Die(float projectileLifeTime) {
        Destroy(this.gameObject, projectileLifeTime);
    }

 
}
