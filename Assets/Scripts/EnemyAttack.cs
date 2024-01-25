using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    

    [SerializeField] float closeRange = 2f;
    [SerializeField] float longRange = 8f;
    [SerializeField] float shootForce;

    [SerializeField] GameObject weapon;
    [SerializeField] GameObject projectile;
    [SerializeField] EnemyRange enemyrange;

    PlayerMovement player;
    Transform target;

    bool isAttacking;
    bool closeWeapon;
    string weaponName;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
        target = player.transform;
        weaponName = weapon.tag;
        CheckWeapon();
    }

    void CheckWeapon() 
    {
        if(weaponName == "Sword" || weaponName == "Axe" || weaponName == "Cross") 
        { 
            closeWeapon = true;
            enemyrange.range.radius = closeRange;
        }
        else if(weaponName == "CrossBow") 
        {
            closeWeapon = false;
            enemyrange.range.radius = longRange;
            Debug.Log("RongRange");
        }
        else 
        { 
            weaponName = null;
            Debug.Log("Varning: No Weapon!");
        }
        
    }

    void SlashOrShot() 
    {
        if (isAttacking) { return; }

        if(closeWeapon) { StartCoroutine(Slash()); }
        if(weaponName == "CrossBow") { StartCoroutine(Shoot()); }
    }   

    private IEnumerator Slash()
    {
        isAttacking = true;
        //player.Death();
        yield return new WaitForSeconds(2);
        isAttacking = false;
        SlashOrShot();
      

    }

    IEnumerator Shoot() 
    {
        isAttacking = true;
        Instantiate(projectile);

        Vector3 direction = gameObject.transform.position;

        float angle = Mathf.Rad2Deg * (Mathf.Atan2(direction.y, direction.x));
        GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, 270 + angle));
        newProjectile.GetComponent<Rigidbody2D>().AddForce(direction.normalized * shootForce);

        yield return new WaitForSeconds (2);
        SlashOrShot();


    }


}


