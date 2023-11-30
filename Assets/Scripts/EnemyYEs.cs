using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyYEs : MonoBehaviour
{
    [Header("General")]
    [SerializeField] Transform player;
    [SerializeField] float speed = 2f;

    [Header("Camera Shake")]
    [SerializeField] float killShakeAmount = 5;
    [SerializeField] float killShakeDuration = 2;

    [Header("Layermasks")]
    [SerializeField] LayerMask arrowLayer;

    [SerializeField] LayerMask projectileLayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
        if (other.CompareTag("Player") || other.gameObject.layer == arrowLayer || other.gameObject.layer == projectileLayer)
        {
            TakeDamage();

            Debug.Log(other.name);

        }
    }

    public void TakeDamage() 
    {
        FindObjectOfType<FollowTarget>().StartShake(killShakeAmount, killShakeDuration);
        Debug.Log(FindObjectOfType<FollowTarget>());
        FindObjectOfType<PowerManager>().AddHoliness(20f);
        Death();
    }

    private void Death()
    {
        FindObjectOfType<BloodManager>().SpawnBlood(gameObject.transform);

        gameObject.SetActive(false);                                                    
    }
}
