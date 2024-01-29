using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using static UnityEngine.ParticleSystem;

public class EnemyYEs : MonoBehaviour
{
    [Header("General")]
    [SerializeField] Transform player;
    [SerializeField] GameObject weapon;
    [SerializeField] float speed = 2f;

    [Header("Camera Shake")]
    [SerializeField] float killShakeAmount = 5;
    [SerializeField] float killShakeDuration = 2;

    [Header("Layermasks")]
    [SerializeField] LayerMask arrowLayer;
    [SerializeField] LayerMask projectileLayer;

    [Header("ParticleSystem")]
    [SerializeField] GameObject particleParent;
    [SerializeField] ParticleSystem deathParticles;


    PowerManager powerManager;
    FollowTarget followTarget;

    void Start()
    {
        powerManager = FindObjectOfType<PowerManager>();
        followTarget = FindObjectOfType<FollowTarget>();
    }

    void Update()
    {       
       //transform.position = Vector2.MoveTowards(gameObject.transform.position, player.position, speed * Time.deltaTime);
       //Look();
    }

    void Look()
    {
        Vector3 lookAt = player.position;

        float AngleRad = Mathf.Atan2(lookAt.y - this.transform.position.y, lookAt.x - this.transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;

        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TakeDamage();

            Debug.Log(other.name);
        }
    }
    public void TakeDamage() 
    {
        GameObject particle = Instantiate(deathParticles.gameObject, transform.position, Quaternion.identity);
        particle.transform.parent = GameObject.FindGameObjectWithTag("Particles").transform;

        followTarget.StartShake(killShakeAmount, killShakeDuration);

        FindObjectOfType<PowerManager>().AddHoliness(20f);

        powerManager.KillCount = powerManager.KillCount + 1;
        Death();
    }

    private void Death()
    {
        FindObjectOfType<BloodManager>().SpawnBlood(gameObject.transform);

        gameObject.SetActive(false);                                                    
    }
}
