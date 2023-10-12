using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
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
                // Shooting
                Debug.Log("isshooting");
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
