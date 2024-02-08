using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class EnemyYEs : MonoBehaviour
{
    [Header("Enemy Type")]
    [SerializeField] bool isRangedEnemy;

    [Header("General")]
    [SerializeField] Transform player;
    [SerializeField] GameObject weapon;
    [SerializeField] float speed = 2f;
    [SerializeField] GameObject droppedWeapon;

    [Header("Arrow")]
    [SerializeField] float shootForce;
    [SerializeField] GameObject arrow;

    [Header("Camera Shake")]
    [SerializeField] float killShakeAmount = 5;
    [SerializeField] float killShakeDuration = 2;

    [Header("Layermasks")]
    [SerializeField] LayerMask arrowLayer;
    [SerializeField] LayerMask projectileLayer;
    [SerializeField] LayerMask playerLayer;

    [Header("Attack Parameters")]
    [SerializeField] float attackRange;
    [SerializeField] float attackCoolDown;

    [Header("ParticleSystem")]
    [SerializeField] GameObject particleParent;
    [SerializeField] ParticleSystem deathParticles;

    [Header("WeaponSprites")]
    [SerializeField] Sprite swordSprite;
    [SerializeField] Sprite axeSprite;
    [SerializeField] Sprite holyCrossSprite;
    [SerializeField] Sprite crossbowSprite;

    string droppedWeaponTag;
    bool canShootArrow = true;
    Sprite weaponSprite;
    BoxCollider2D attackCollider;

    PowerManager powerManager;
    FollowTarget followTarget;
    AudioSource audioSource;
    Animator spriteAnimator;
    EnemyBehavior enemyBehavior;
    SFXManager sfxManager;

    void OnEnable()
    {
        if (gameObject.transform.GetChild(1) != null && gameObject.name == "AttackCollider")
        {
            string message = $"Can't find \"AttackCollider\" on {gameObject}!" + "\nPlease make sure there is a AttackCollider object childed to this object with a BoxCollider2D";
            Debug.LogWarning(message, gameObject);
            return;
        }
        attackCollider = gameObject.transform.GetChild(1).GetComponent<BoxCollider2D>();
        spriteAnimator = transform.GetComponentInChildren<Animator>();
        enemyBehavior = transform.GetComponent<EnemyBehavior>();
        powerManager = FindObjectOfType<PowerManager>();
        followTarget = FindObjectOfType<FollowTarget>();
        audioSource = GetComponent<AudioSource>();
        sfxManager = FindAnyObjectByType<SFXManager>();

        if (weapon == null) { return; }
        
        switch (weapon.tag)
        {
            case "Sword":
                spriteAnimator.SetBool("Sword", true);

                droppedWeaponTag = "Sword";
                weaponSprite = swordSprite;

                break;

            case "Axe":
                spriteAnimator.SetBool("Axe", true);

                droppedWeaponTag = "Axe";
                weaponSprite = axeSprite;

                break;

            case "CrossBow":
                spriteAnimator.SetBool("Crossbow", true);


                droppedWeaponTag = "CrossBow";
                weaponSprite = crossbowSprite;

                break;

            case "Cross":
                spriteAnimator.SetBool("Cross", true);


                droppedWeaponTag = "Cross";
                weaponSprite = holyCrossSprite;

                break;

            default:
                Debug.Log("something went wrong");
                break;
        }
    }

    void Update()
    {
        //transform.position = Vector2.MoveTowards(gameObject.transform.position, player.position, speed * Time.deltaTime);
        //Look();
        if (isRangedEnemy) { RangedCheck(); }
        
        else if (!isRangedEnemy) { AttackCheck(); }
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
        //audioSource.Play();
        sfxManager.EnemyDeathSound();

        if (weaponSprite != null)
        {
            Debug.Log("Weaponsprite is " + weaponSprite.name);
            GameObject newProjectile = Instantiate(droppedWeapon, transform.position, transform.rotation);

            newProjectile.transform.GetChild(0).gameObject.tag = droppedWeaponTag;
            newProjectile.GetComponentInChildren<SpriteRenderer>().sprite = weaponSprite;
            newProjectile.GetComponent<WeaponProjectile>().Velocity(0);
            newProjectile.GetComponent<WeaponProjectile>().GroundWeapon();
        }

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

        float dist = Vector3.Distance(player.position, gameObject.transform.position);

        //make attack ray instead

        

        RaycastHit2D attackRay = Physics2D.Raycast(transform.position, transform.position - attackCollider.transform.position, attackRange, playerLayer);
        Debug.DrawLine(transform.position, attackRay.point, Color.red);

        Debug.Log("cast ray");

        if (attackRay.collider != null)
        {
            Debug.Log("Hit player");

            spriteAnimator.SetTrigger("Attack");

            FindObjectOfType<PlayerMovement>().Death();
            yield return null;

        }

        yield return null;
        

    }

    void RangedCheck()
    {
        if (enemyBehavior.isChasingTarget && canShootArrow)
        {
            canShootArrow = false;
            Debug.Log("Enemy is shooting arrow and is chasing target is" + enemyBehavior.isChasingTarget );

            StartCoroutine(ShootArrow());
        }
    }

    IEnumerator ShootArrow()
    {
        Vector3 direction = attackCollider.transform.position - transform.position;

        float angle = Mathf.Rad2Deg * (Mathf.Atan2(direction.y, direction.x));

        GameObject newProjectile = Instantiate(arrow, attackCollider.transform.position, Quaternion.Euler(0, 0, 270 + angle));

        //newProjectile.gameObject.layer = LayerMask.GetMask("EnemyArrow");

        newProjectile.GetComponent<Rigidbody2D>().AddForce(direction.normalized * shootForce);

        yield return new WaitForSeconds(attackCoolDown);

        canShootArrow = true;
    }


}
