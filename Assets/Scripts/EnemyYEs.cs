using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyYEs : MonoBehaviour
{
    [Header("Enemy Type")]
    [SerializeField] bool isRangedEnemy;
    [SerializeField] bool isDemonEnemy;

    [Header("General")]
    [SerializeField] Transform player;
    [SerializeField] GameObject weapon;
    [SerializeField] float speed = 2f;
    [SerializeField] float knockbackRange = 15;
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

    Vector2 knockbackVector;

    string droppedWeaponTag;
    bool canShootArrow = true;
    bool once = true;
    bool once2 = true;
    bool once3 = true;
    Sprite weaponSprite;
    BoxCollider2D attackCollider;
    Rigidbody2D rigidbody;

    PowerManager powerManager;
    FollowTarget followTarget;
    AudioSource audioSource;
    Animator spriteAnimator;
    Animator stunAnimator;
    EnemyBehavior enemyBehavior;
    SFXManager sfxManager;
    Attack playerAttack;
    FollowMouse followMouse;

    public bool Punched { get; private set; } = false;

    void OnEnable()
    {
        player = FindObjectOfType<PlayerMovement>().transform;

        if (gameObject.transform.GetChild(2) != null && gameObject.name == "AttackCollider")
        {
            string message = $"Can't find \"AttackCollider\" on {gameObject}!" + "\nPlease make sure there is a AttackCollider object childed to this object with a BoxCollider2D";
            Debug.LogWarning(message, gameObject);
            return;
        }
        attackCollider = gameObject.transform.GetChild(2).GetComponent<BoxCollider2D>();
        spriteAnimator = transform.GetComponentInChildren<Animator>();
        stunAnimator = transform.GetChild(1).GetComponent<Animator>();
        enemyBehavior = transform.GetComponent<EnemyBehavior>();
        powerManager = FindObjectOfType<PowerManager>();
        followTarget = FindObjectOfType<FollowTarget>();
        audioSource = GetComponent<AudioSource>();
        sfxManager = FindAnyObjectByType<SFXManager>();
        playerAttack = FindObjectOfType<Attack>();
        rigidbody = GetComponent<Rigidbody2D>();
        followMouse = FindObjectOfType<FollowMouse>();

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
        if (player == null) { return; }

        //transform.position = Vector2.MoveTowards(gameObject.transform.position, player.position, speed * Time.deltaTime);
        //Look();
        if (isRangedEnemy && spriteAnimator.GetBool("Crossbow")) { RangedCheck(); }
        
        else if (!isRangedEnemy) { StartAttackEncounter(); }

    }

    void Look()
    {
        Vector3 lookAt = player.position;

        float AngleRad = Mathf.Atan2(lookAt.y - this.transform.position.y, lookAt.x - this.transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;

        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
    }

    public void TakeDamage() 
    {
        if (playerAttack.PlayerIsPunching) // Player is punching enemy -> Enemy Knockback! 
        {
            Knockback();
        }
        else if (once2) // Player is attacking enemy -> Enemy simply dies!
        {
            once2 = false;

            DropWeapon();

            GameObject particle = Instantiate(deathParticles.gameObject, transform.position, Quaternion.identity);
            particle.transform.parent = GameObject.FindGameObjectWithTag("Particles").transform;

            FindObjectOfType<PowerManager>().AddHoliness(20f);

            powerManager.KillCount = powerManager.KillCount + 1;
            sfxManager.EnemyDeathSound();
            Death();

        }
    }

    private void Death()
    {
        FindObjectOfType<BloodManager>().SpawnBlood(gameObject.transform);

        gameObject.SetActive(false);
    }

    void StartAttackEncounter() 
    {
        if (once3)
        {
            once3 = false;
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack() 
    {
        yield return new WaitForSeconds(attackCoolDown);

        if (weapon == null) { yield break; }

        GameObject attackCollider = gameObject.transform.GetChild(2).gameObject;
        float dist = Vector3.Distance(player.position, gameObject.transform.position);

        if (dist > attackRange || playerAttack.PlayerIsAttacking || playerAttack.PlayerIsPunching)
        {
            attackCollider.SetActive(false);
        }

        else if (dist <= attackRange)
        {
            Debug.Log("Playerattacking is " + playerAttack.PlayerIsAttacking + " Enemy is attacking");

            attackCollider.SetActive(true);
        }

        once3 = true;
    }

    void RangedCheck()
    {
        if (enemyBehavior.isChasingTarget && canShootArrow)
        {
            canShootArrow = false;

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

    IEnumerator PunchedCooldown()
    {
        Punched = true;
        stunAnimator.SetBool("Stun", true);

        yield return new WaitForSeconds(3);

        Punched = false;
        stunAnimator.SetBool("Stun", false);
    }

    private void DropWeapon()
    {
        if (weaponSprite != null && once) // Enemy drops its weapon...
        {
            once = false;

            followTarget.StartShake(killShakeAmount, killShakeDuration);

            GameObject newProjectile = Instantiate(droppedWeapon, transform.position, transform.rotation);

            newProjectile.transform.GetChild(0).gameObject.tag = droppedWeaponTag;
            newProjectile.GetComponentInChildren<SpriteRenderer>().sprite = weaponSprite;
            newProjectile.GetComponent<WeaponProjectile>().Velocity(0);
            newProjectile.GetComponent<WeaponProjectile>().GroundWeapon();
            newProjectile.transform.localScale = new Vector2(0.8f, 0.8f);

            spriteAnimator.SetBool("Sword", false);
            spriteAnimator.SetBool("Axe", false);
            spriteAnimator.SetBool("Crossbow", false);
            spriteAnimator.SetBool("Cross", false);
            weapon = null;
        }
    }

    public void Knockback()
    {
        DropWeapon();
        StartCoroutine(PunchedCooldown());
    }
}
