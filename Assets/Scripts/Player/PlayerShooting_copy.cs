using UnityEngine;
using System.Collections;
using System;

public class PlayerShooting_copy : MonoBehaviour {

    public float shootGapTime = 0.15f;
    public int damagePerShot = 20;
    public float range = 100f;

    float timer; 
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunPartical;
    LineRenderer gunLine;
    Light gunLight;
    AudioSource gunClip;

    float effectDelayTime = 0.2f;

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunPartical = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();
        gunClip = GetComponent<AudioSource>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= shootGapTime && Input.GetButton("Fire1") && Time.timeScale != 0)
            Shoot();

        if (timer >= shootGapTime * effectDelayTime)
            DisableEffects();
    }

    private void DisableEffects()
    {
        gunLight.enabled = false;
        gunLine.enabled = false;
    }

    private void Shoot()
    {
        timer = 0f;

        gunClip.Play();

        gunLight.enabled = true;
        gunPartical.Stop();
        gunPartical.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            gunLine.SetPosition(1, shootHit.point);
        } else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
}
