using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] GameObject throwingProjectile;
    [SerializeField] float pickupDistance = 1;
    [SerializeField] float throwPower = 70;
    [SerializeField] LayerMask gunLayer;

    bool hasWeapon = false;

    SlotMelee slotMelee;
    WeaponProjectile weaponProjectile;
    FollowMouse followMouse;
    Sprite gunSprite;
    Sprite rangeSprite;


    void Start()
    {
        slotMelee = FindObjectOfType<SlotMelee>();
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

            Debug.Log("ray detected something");

            if (ray.collider.gameObject.CompareTag("Melee"))
            {
                weaponProjectile = ray.collider.gameObject.GetComponent<WeaponProjectile>();

                gunSprite = ray.collider.gameObject.GetComponent<SpriteRenderer>().sprite;

                slotMelee.AddWeapon(gunSprite);
            }
            else if (ray.collider.gameObject.CompareTag("Range"))
            {
                rangeSprite = ray.collider.gameObject.GetComponent<SpriteRenderer>().sprite;




            }



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

        slotMelee.RemoveWeapon();

        GameObject newProjectile = Instantiate(throwingProjectile, transform.position, transform.rotation);

        newProjectile.GetComponent<SpriteRenderer>().sprite = gunSprite;

        newProjectile.GetComponent<Rigidbody2D>().AddForce(followMouse.MousePosition() * throwPower);
    }
}
