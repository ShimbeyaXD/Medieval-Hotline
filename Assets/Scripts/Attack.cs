using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] float attackRange = 3;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] LayerMask wallLayer;

    [SerializeField] GameObject arrow;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject rangeWeapon;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] float shootForce = 4000;
    [SerializeField] int maxArrows = 5;
    [SerializeField] GameObject weaponObject;
    
    NewWeaponManager newWeaponManager;
    FollowMouse followMouse;
    FollowTarget followTarget;

    public int CurrentArrows { get; set; } = 5;

    void Start()
    {
        boxCollider.enabled = false;
        newWeaponManager = FindObjectOfType<NewWeaponManager>();
        followMouse = FindObjectOfType<FollowMouse>();
        followTarget = FindObjectOfType<FollowTarget>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && newWeaponManager.AnyWeapon)
        {
            if (newWeaponManager.HasWeapon)
            {
                EnableMelee();
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
        newWeaponManager.SetAttackAnimator();
        FindObjectOfType<SFXManager>().PlaySFX("slash");
        weaponObject.SetActive(false);
    }

    public void DisableMelee() // trigger from animation events
    {
        StopCoroutine(AttackRay());

        //boxCollider.enabled = false;
    }

    IEnumerator AttackRay()
    {
        if (true)
        {
            RaycastHit2D ray = Physics2D.Raycast(transform.position, followMouse.MousePosition() - (Vector2)transform.position, attackRange, enemyLayer);
            
            if (ray.collider != null)
            {
                Debug.Log(ray.collider.gameObject.name);
                ray.collider.gameObject.GetComponent<EnemyYEs>().TakeDamage();
            }
            yield return null;
        }
    }
}
