using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject rangeWeapon;
    [SerializeField] float shootForce = 4000;

    BoxCollider2D boxCollider;
    WeaponManager weaponManager;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
        weaponManager = FindObjectOfType<WeaponManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && weaponManager.HasWeapon()) 
        {
            if (weaponManager.HasMelee() )
            {
                EnableMelee();
            }
            else
            {
                Vector3 direction = rangeWeapon.transform.position - transform.position;

                float angle = Mathf.Rad2Deg * (Mathf.Atan2(direction.y, direction.x));

                GameObject newProjectile = Instantiate(arrow, rangeWeapon.transform.position, Quaternion.Euler(0,0, 270 + angle));

                newProjectile.GetComponent<Rigidbody2D>().AddForce(direction.normalized * shootForce);
            }

        }
    }

    public void EnableMelee()
    {
        boxCollider.enabled = true;
    }

    public void DisableMelee()
    {
        boxCollider.enabled = false;
    }
}
