using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] GameObject throwingProjectile;
    [SerializeField] float pickupDistance = 1;
    [SerializeField] float throwPower = 70;
    [SerializeField] LayerMask gunLayer;

    bool hasWeapon = false;

    Weapon weapon;
    WeaponProjectile weaponProjectile;
    FollowMouse followMouse;
    Sprite gunSprite;


    void Start()
    {
        weapon = FindObjectOfType<Weapon>();
        followMouse = FindObjectOfType<FollowMouse>();
    }

    void Update()
    {
        if (Input.GetMouseButton(1) && hasWeapon) { WeaponThrow(); }
        if (Input.GetKeyDown(KeyCode.E) && !hasWeapon) { WeaponPickup(); }
    }

    void WeaponPickup()
    {
        RaycastHit2D ray = Physics2D.BoxCast(transform.position, new Vector2(2, 2), 0, Vector2.up, pickupDistance, gunLayer);
        
        if (ray.collider != null)
        {
            hasWeapon = true;

            weaponProjectile = ray.collider.gameObject.GetComponent<WeaponProjectile>();

            gunSprite = ray.collider.gameObject.GetComponent<SpriteRenderer>().sprite;

            Debug.Log("ray detected something");

            weapon.AddWeapon(gunSprite);

            Destroy(ray.collider.gameObject);
        }
        else
        {
            Debug.Log("didnt hit");
        }
    }
    // https://discussions.unity.com/t/c-weapon-pickup-script/213361/2

    void WeaponThrow()
    {
        Debug.Log("threw " + gunSprite.name);

        hasWeapon = false;

        weapon.RemoveWeapon();

        GameObject newProjectile = Instantiate(throwingProjectile, transform.position, transform.rotation);

        newProjectile.GetComponent<SpriteRenderer>().sprite = gunSprite;

        newProjectile.GetComponent<Rigidbody2D>().AddForce(followMouse.MousePosition() * throwPower);
    }
}
