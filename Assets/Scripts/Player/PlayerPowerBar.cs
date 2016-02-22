using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class PlayerPowerBar : MonoBehaviour {

    public int startPower = 0;
    public int currPower;
    public Slider powerSlider;

    Animator anim;
    bool isFullPower;

    void Awake()
    {
        anim = powerSlider.transform.GetComponentInParent<Animator>() ;
        currPower = startPower;
        powerSlider.value = currPower;
        isFullPower = false;
    }

    void Update()
    {

        if (isFullPower && Input.GetButtonDown("Fire2"))
            DestroyAllEnemies();
            
    }
    
    private void DestroyAllEnemies()
    {
        GameObject[] toBeDestroy = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < toBeDestroy.Length; i++)
        {
            EnemyHealth enemyHealth = toBeDestroy[i].GetComponent<EnemyHealth>();
            Rigidbody rigidbody = toBeDestroy[i].GetComponent<Rigidbody>();
            enemyHealth.TakeDamage(enemyHealth.currentHealth, rigidbody.position);
        }
        anim.SetBool("IsFullPower", false);
        isFullPower = false;
        currPower = startPower;
    }

    public void IncreasePower(int amount)
    {
        if (currPower >= powerSlider.maxValue)
        {
            isFullPower = true;
            anim.SetBool("IsFullPower", true);
            return;
        }
        currPower += amount;
        powerSlider.value = currPower;
    }
}
