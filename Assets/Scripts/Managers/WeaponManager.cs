using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour {

    public Slider healthSilder;

    GameObject player;
    PlayerHealth playerHealth;
    Animator anim;
    int levelScore;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("player");
        playerHealth = player.GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
        levelScore = ScoreManager.score;
    }

    void Update()
    {
        if ((ScoreManager.score - levelScore) >= 100 && ScoreManager.score != 0)
        {
            PlayerShooting.damagePerShot += 20;
            if (playerHealth.currentHealth < 100)
            {
                playerHealth.currentHealth += 10;
                healthSilder.value = playerHealth.currentHealth;
            }
                
            Debug.Log(PlayerShooting.damagePerShot);
            anim.SetTrigger("GunUpgrade");
            levelScore = ScoreManager.score;
        }
    }
}
