using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

// WHo named thing monstrostity
public class EnemyYEs : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float speed = 2f;

    [Header("Camera Shake")]
    [SerializeField] float killShakeAmount = 5;
    [SerializeField] float killShakeDuration = 2;

    [Header("Layermasks")]
    [SerializeField] LayerMask arrowLayer;
    [SerializeField] LayerMask projectileLayer;

    [SerializeField] GameObject weapon;

    PowerManager powerManager;

    [SerializeField] LayerMask projectileLayer;

    // Start is called before the first frame update
    void Start()
    {
        powerManager = FindObjectOfType<PowerManager>();
    }

    void Update()
    {
       
       transform.position = Vector2.MoveTowards(gameObject.transform.position, player.position, speed * Time.deltaTime);
        Look();
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
        if (other.CompareTag("Player") || other.gameObject.layer == projectileLayer)
        {
            Debug.Log("it");
            TakeDamage();

            Debug.Log(other.name);
        }
    }

    public void TakeDamage() 
    {
        FindObjectOfType<FollowTarget>().StartShake(killShakeAmount, killShakeDuration);
        FindObjectOfType<PowerManager>().AddHoliness(20f);
        powerManager.KillCount = powerManager.KillCount + 1;
        Death();
    }

    private void Death()
    {
        FindObjectOfType<BloodManager>().SpawnBlood(transform);
        gameObject.SetActive(false);                                                    
    }
}
