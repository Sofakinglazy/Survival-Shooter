using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class WeaponManager : MonoBehaviour {

    public Slider healthSilder;
    public int scorePerUpdate = 100;

    GameObject player;
    PlayerHealth playerHealth;
    Animator anim;
    int currScore;
    int lastScore;
    int level;
    int levelScore;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("player");
        playerHealth = player.GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
        UpdateLevelScore();
    }

    void Update()
    {
        currScore = ScoreManager.score;
        if ((currScore - lastScore) >= levelScore && currScore != 0)
        {
            PlayerShooting.damagePerShot += 20;
            RestorePlayerHealth();
            anim.SetTrigger("GunUpgrade");
            lastScore = currScore;
            level++;
        }
        UpdateLevelScore();
    }

    private void RestorePlayerHealth()
    {
        if (playerHealth.currentHealth < 100)
        {
            playerHealth.currentHealth += 10;
            healthSilder.value = playerHealth.currentHealth;
        }
    }

    void UpdateLevelScore()
    {
        levelScore = (int)(Mathf.Log(level + 2, 2)) * scorePerUpdate;
    }
}
