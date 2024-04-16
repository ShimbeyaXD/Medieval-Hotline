using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] float attackRange = 4f;
    [SerializeField] float shootForce = 4000;
    [SerializeField] int maxArrows = 5;
    [SerializeField] float knockbackCooldown = 3;
    [SerializeField] float chargeTime = 3;

    [Header("Attack Objects")]
    [SerializeField] GameObject weaponObject;
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject rangeWeapon;

    [Header("Layer")]
    [SerializeField] LayerMask enemyLayer;

    [SerializeField] LayerMask wallLayer;

    [Header("Components")]
    [SerializeField] BoxCollider2D boxCollider;

    RaycastHit2D attackRay;
    bool isCastingRay = false;
    bool isCharging = false;

    NewWeaponManager newWeaponManager;
    PlayerLook playerLook;
    FollowMouse followMouse;
    FollowTarget followTarget;
    PlayerMovement playerMovement;
    Rigidbody2D myRigidbody;

    public int CurrentArrows { get; set; } = 5;

    public bool PlayerIsPunching { get; private set; } = false;

    public bool PlayerIsAttacking { get; private set; } = false;

    public bool PlayerIsCharging { get; private set; } = false;


    void Start()
    {
        boxCollider.enabled = false;
        newWeaponManager = FindObjectOfType<NewWeaponManager>();
        playerLook = GetComponent<PlayerLook>();
        followMouse = FindObjectOfType<FollowMouse>();
        followTarget = FindObjectOfType<FollowTarget>();
        playerMovement = GetComponent<PlayerMovement>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (playerMovement.Dead) return;
        
        CurrentArrows = (int)Mathf.Clamp(CurrentArrows, 0, Mathf.Infinity);

        /*
        if (Input.GetMouseButton(0) && !newWeaponManager.AnyWeapon && !PlayerIsPunching) // Is punching
        {
            StartCoroutine(KnockbackCooldown());
        }
        */


        if (PlayerIsAttacking && !isCharging)
        {
            StopCoroutine(ChargingTime());
        }

        if (Input.GetButtonDown("Fire1") && newWeaponManager.AnyWeapon) // Fire
        {
            if (newWeaponManager.HasWeapon)
            {
                EnableMelee();
                PlayerIsAttacking = true;
                //FindObjectOfType<SFXManager>().PlaySFX("slash");
                newWeaponManager.SetAttackAnimator();
            }
            if (newWeaponManager.HasCrossbow) 
            {
                if (CurrentArrows-- > 0)
                {
                    ShootArrow(arrow);
                }
            }
            if (newWeaponManager.HasGlock)
            {
                if (CurrentArrows-- > 0)
                {
                    followTarget.StartShake(0.4f, 1.7f);
                    //FindObjectOfType<SFXManager>().PlaySFX("Gun");
                    ShootArrow(bullet);
                }
            }
        }
    }

    void ShootArrow(GameObject projectile)
    {
        Vector3 direction = followMouse.MousePosition() - (Vector2)transform.position;

        float angle = Mathf.Rad2Deg * (Mathf.Atan2(direction.y, direction.x));

        GameObject newProjectile = Instantiate(projectile, rangeWeapon.transform.position, Quaternion.Euler(0, 0, 270 + angle));

        newProjectile.GetComponent<Rigidbody2D>().AddForce(direction.normalized * shootForce);
    }


    public void ResetArrows()
    {
        CurrentArrows = 5;
    }

    public void EnableMelee()
    {
        StartCoroutine(AttackRay());

        //boxCollider.enabled = true;
        weaponObject.SetActive(false);
    }

    public void DisableMelee() // trigger from animation events
    {
        PlayerIsPunching = false;
        PlayerIsAttacking = false;
        StopCoroutine(AttackRay());

        //boxCollider.enabled = false;
    }

    public void EnableCharge()
    {
        StartCoroutine(ChargingTime());
        StartCoroutine(Charge());
    }

    IEnumerator AttackRay()
    {
        SoundManager.PlaySound("Attack");
        while (true)
        {
            Vector2 mousePosition = followMouse.MousePosition();
            Vector2 offset = ((mousePosition - (Vector2)transform.position).normalized);
            Vector2 boxPosition = (Vector2)transform.position + offset;

            Collider2D[] colliders = Physics2D.OverlapBoxAll(boxPosition, new Vector2(attackRange, attackRange), 0);

            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.CompareTag("Wall") || collider.gameObject.CompareTag("Door"))
                {
                    yield break;
                }
                else if (collider.gameObject.CompareTag("Enemy"))
                {
                    collider.gameObject.GetComponent<EnemyYEs>().TakeDamage();
                    yield return new WaitForEndOfFrame();
                }

            }
            if (!PlayerIsAttacking) { yield break; }

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator KnockbackCooldown()
    {
        PlayerIsPunching = true;
        EnableMelee();
        newWeaponManager.SetPunchAnimator();

        yield return new WaitForSeconds(knockbackCooldown);

        PlayerIsPunching = false;
    }

    IEnumerator Charge()
    {
        while (true)
        {
            float offsetMagnitude = 1.5f;

            Vector2 mousePosition = followMouse.MousePosition();
            Vector2 offset = (mousePosition - (Vector2)transform.position).normalized * offsetMagnitude;
            Vector2 boxPosition = (Vector2)transform.position + offset;
            
            Collider2D[] colliders = Physics2D.OverlapBoxAll(boxPosition, new Vector2(2,2), 0);

            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.CompareTag("Wall") || collider.gameObject.CompareTag("Door"))
                {
                    ChangeCharge(false);
                    yield break;
                }
                else if (collider.gameObject.CompareTag("Enemy"))
                {
                    collider.gameObject.GetComponent<EnemyYEs>().TakeDamage();
                }
            }
            if (!isCharging) { yield break; }

            yield return new WaitForEndOfFrame();
        }
    }

    void ChangeCharge(bool state)
    {
        isCharging = state;
        PlayerIsPunching = state;
        PlayerIsAttacking = state;
        playerMovement.ChangeSpeed(state);
        newWeaponManager.SetChargingAnimator(state);
        followMouse.FreezeMouse = state;
        PlayerIsCharging = state;
    }

    IEnumerator ChargingTime()
    {
        ChangeCharge(true);

        /*
        isCharging = true;
        PlayerIsPunching = true;
        PlayerIsAttacking = true;
        playerMovement.ChangeSpeed(true);
        newWeaponManager.SetChargingAnimator(true);
        */


        yield return new WaitForSeconds(chargeTime);

        ChangeCharge(false);


        /*
        isCharging = false;
        PlayerIsAttacking = false;
        PlayerIsPunching = false;
        playerMovement.ChangeSpeed(false);
        newWeaponManager.SetChargingAnimator(false);
        */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy") 
        { 
            collision.GetComponent<EnemyYEs>().Knockback(); 
        }
    }
}
