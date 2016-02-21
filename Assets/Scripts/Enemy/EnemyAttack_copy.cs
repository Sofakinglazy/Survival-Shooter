using UnityEngine;
using System.Collections;
using System;

public class EnemyAttack_copy : MonoBehaviour {

    public int damage = 10;
    public float attackGapTime = 0.5f;

    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    float timer;
    bool playerInRange;

    void Awake()
    {
        timer = Time.time;
        player = GameObject.FindGameObjectWithTag("player");
        playerHealth = player.GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
            playerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
            playerInRange = false;
    }

    void Update()
    {
        if (playerInRange && Time.time - timer >= attackGapTime) 
            Attack();
        if (playerHealth.currentHealth <= 0)
            anim.SetTrigger("PlayerDead");
    }

    private void Attack()
    {
        timer = Time.time;
        if (playerHealth.currentHealth > 0)
            playerHealth.TakeDamage(damage);
    }
}
