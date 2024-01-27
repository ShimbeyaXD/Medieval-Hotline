using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
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

    [SerializeField] float attackRange;
    [SerializeField] float attackCoolDown;

    [SerializeField] GameObject weapon;

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

        AttackCheck();
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

    void AttackCheck() 
    { 
      GameObject attackCollider = gameObject.transform.GetChild(0).gameObject;
        float dist = Vector3.Distance(player.position, gameObject.transform.position);
       
      if(dist < attackRange) { StartCoroutine(Attack()); }
       
    }

    IEnumerator Attack() 
    {
        Debug.Log("Attack");
      new WaitForSeconds(attackCoolDown);

        GameObject attackCollider = gameObject.transform.GetChild(0).gameObject;
        float dist = Vector3.Distance(player.position, gameObject.transform.position);

        if (dist < attackRange) 
        {
            FindObjectOfType<PlayerMovement>().Death();
            yield return null;

        }

        yield return null;
        

    }


}
