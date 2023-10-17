using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject rangeWeaponPosition;

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
                //Instantiate(arrow, )

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
