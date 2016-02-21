using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class WeaponManager : MonoBehaviour {

    public Slider healthSilder;

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
        //lastScore = ScoreManager.score;
        UpdateLevelScore();
    }

    void Update()
    {
        currScore = ScoreManager.score;
        Debug.Log(currScore - lastScore);
        if ((currScore - lastScore) >= levelScore && currScore != 0)
        {
            PlayerShooting.damagePerShot += 20;
            RestorePlayerHealth();
            //Debug.Log(currScore - lastScore);
            //Debug.Log(PlayerShooting.damagePerShot);
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
        levelScore = (int)(Mathf.Log(level + 2, 2)) * 10;
    }
}
