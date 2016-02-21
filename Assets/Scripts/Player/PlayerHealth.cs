using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class PlayerHealth : MonoBehaviour
{
    public int startHealth = 100;
    public int currentHealth;
    public Slider healthSilder;
    public Image damageImage;
    public Color flashColor = new Color(1f, 0f, 0f, 1f);
    public float flashSpeed = 5f;
    public AudioClip deadClip;

    private AudioSource playerAudio; 
    private Animator animator;
    private PlayerMovement playerMovement;
    private PlayerShooting playerShooting;
    private bool damage;
    private bool isDead;

    void Awake()
    {
        playerAudio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
        currentHealth = startHealth;
    }

    void Update()
    {
        if (damage)
            damageImage.color = flashColor;
        else
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        damage = false;
    }

    public void TakeDamage(int loss)
    {
        damage = true;
        currentHealth -= loss;
        healthSilder.value = currentHealth;
        playerAudio.Play();
        if (currentHealth == 0 && !isDead)
            SetDead();
    }

    void SetDead()
    {
        isDead = true;
        playerAudio.clip = deadClip;
        playerAudio.Play();
        playerShooting.DisableEffects();
        playerShooting.enabled = false;
        animator.SetTrigger("Die");
        playerMovement.enabled = false;
        
    }
}
