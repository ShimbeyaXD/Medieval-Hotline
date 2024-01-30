using System.Collections;
using UnityEngine;

public class EnemyYEs : MonoBehaviour
{
    [Header("General")]
    [SerializeField] Transform player;
    [SerializeField] GameObject weapon;
    [SerializeField] float speed = 2f;
    [SerializeField] GameObject droppedWeapon;

    [Header("Camera Shake")]
    [SerializeField] float killShakeAmount = 5;
    [SerializeField] float killShakeDuration = 2;

    [Header("Layermasks")]
    [SerializeField] LayerMask arrowLayer;
    [SerializeField] LayerMask projectileLayer;

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
    Sprite weaponSprite;

    PowerManager powerManager;
    FollowTarget followTarget;
    AudioSource audioSource;
    Animator spriteAnimator;

    void OnEnable()
    {
        spriteAnimator = transform.GetComponentInChildren<Animator>();
        powerManager = FindObjectOfType<PowerManager>();
        followTarget = FindObjectOfType<FollowTarget>();
        audioSource = FindObjectOfType<AudioSource>();

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
        GameObject particle = Instantiate(deathParticles.gameObject, transform.position, Quaternion.identity);
        particle.transform.parent = GameObject.FindGameObjectWithTag("Particles").transform;

        followTarget.StartShake(killShakeAmount, killShakeDuration);

        FindObjectOfType<PowerManager>().AddHoliness(20f);

        powerManager.KillCount = powerManager.KillCount + 1;
        audioSource.Play();

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
