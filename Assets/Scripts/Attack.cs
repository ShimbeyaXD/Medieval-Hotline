using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] float attackRange = 4f;
    [SerializeField] float shootForce = 4000;
    [SerializeField] int maxArrows = 5;
    [SerializeField] float knockbackCooldown = 3;

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

    NewWeaponManager newWeaponManager;
    FollowMouse followMouse;
    FollowTarget followTarget;
    PlayerMovement playerMovement;

    public int CurrentArrows { get; set; } = 5;

    public bool PlayerIsPunching { get; private set; } = false;

    public bool PlayerIsAttacking { get; private set; } = false;


    void Start()
    {
        boxCollider.enabled = false;
        newWeaponManager = FindObjectOfType<NewWeaponManager>();
        followMouse = FindObjectOfType<FollowMouse>();
        followTarget = FindObjectOfType<FollowTarget>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && !newWeaponManager.AnyWeapon && !PlayerIsPunching) // Is punching
        {
            StartCoroutine(KnockbackCooldown());
        }

        if (Input.GetButtonDown("Jump") && !PlayerIsAttacking) // Is charging
        {
            PlayerIsAttacking = true;
            boxCollider.enabled = true;
            playerMovement.ChangeSpeed(true);
            newWeaponManager.SetChargingAnimator();
            
            // enable animation
            // raise the movement speed 

        }
        if (Input.GetButtonUp("Jump") && PlayerIsAttacking)
        {
            PlayerIsAttacking = false;
            boxCollider.enabled = false;
            playerMovement.ChangeSpeed(false);

            Debug.Log("DeCharging");

        }

        if (Input.GetMouseButtonDown(0) && newWeaponManager.AnyWeapon)
        {
            if (newWeaponManager.HasWeapon)
            {
                EnableMelee();
                PlayerIsAttacking = true;
                FindObjectOfType<SFXManager>().PlaySFX("slash");
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
                    followTarget.StartShake(0.2f, 0.5f);
                    FindObjectOfType<SFXManager>().PlaySFX("Gun");
                    ShootArrow(bullet);
                }
            }
        }
    }

    void ShootArrow(GameObject projectile)
    {
        Debug.Log(CurrentArrows);

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

    IEnumerator AttackRay()
    {
        if (true)
        {
            attackRay = Physics2D.Raycast(transform.position, followMouse.MousePosition() - (Vector2)transform.position, attackRange, enemyLayer);
            isCastingRay = true;
            
            if (attackRay.collider != null)
            {
                attackRay.collider.gameObject.GetComponent<EnemyYEs>().TakeDamage();
            }
            yield return null;
        }

        isCastingRay = false;
    }

    IEnumerator KnockbackCooldown()
    {
        PlayerIsPunching = true;
        EnableMelee();
        newWeaponManager.SetPunchAnimator();

        yield return new WaitForSeconds(knockbackCooldown);

        PlayerIsPunching = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy") { collision.GetComponent<EnemyYEs>().Knockback(); }
    }
}
