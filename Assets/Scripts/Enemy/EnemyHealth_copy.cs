using UnityEngine;
using System.Collections;
using System;

public class EnemyHealth_copy : MonoBehaviour {

    public int startHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deadClip;

    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;

    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        currentHealth = startHealth;
    }

    void Update()
    {
        if (isSinking)
            transform.Translate(Vector3.down * sinkSpeed * Time.deltaTime);
    }

    public void TakeDamage(int loss, Vector3 hitPoint)
    {
        if (isDead)
            return;
        currentHealth -= loss;
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();
        enemyAudio.Play();
        if (currentHealth <= 0)
            SetDead();
    }

    void SetDead()
    {
        isDead = true;
        capsuleCollider.isTrigger = true;
        enemyAudio.clip = deadClip;
        enemyAudio.Play();
        anim.SetTrigger("Dead");
    }

    public void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        Destroy(gameObject, 2f);
    }
}
