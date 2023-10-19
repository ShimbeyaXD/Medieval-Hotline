using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] GameObject throwingProjectile;
    [SerializeField] float pickupDistance = 1;
    [SerializeField] float throwPower = 70;
    [SerializeField] LayerMask gunLayer;
    [SerializeField] GameObject weaponObject;

    [SerializeField] Vector3 meleePosition;
    [SerializeField] Vector3 rangePosition;

    bool hasWeapon = false;
    bool hasMelee = false;

    MeleeWeapon meleeWeapon;
    RangeWeapon rangeWeapon;
    WeaponProjectile weaponProjectile;
    FollowMouse followMouse;
    Sprite gunSprite;
    Sprite rangeSprite;
    Attack attack;


    void Start()
    {
        meleeWeapon = FindObjectOfType<MeleeWeapon>();
        rangeWeapon = FindObjectOfType<RangeWeapon>();
        followMouse = FindObjectOfType<FollowMouse>();
        attack = FindObjectOfType<Attack>();
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

            weaponObject = ray.collider.gameObject;

            weaponProjectile = ray.collider.gameObject.GetComponent<WeaponProjectile>();

            Debug.Log("ray detected something");

            if (ray.collider.gameObject.CompareTag("Melee"))
            {
                hasMelee = true;

                gunSprite = ray.collider.gameObject.GetComponent<SpriteRenderer>().sprite;

                meleeWeapon.AddWeapon(gunSprite);
            }
            else if (ray.collider.gameObject.CompareTag("Range"))
            {
                hasMelee = false;

                attack.ResetArrows();

                rangeSprite = ray.collider.gameObject.GetComponent<SpriteRenderer>().sprite;

                rangeWeapon.AddWeapon(rangeSprite);
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
        hasWeapon = false;

        Sprite projectileSprite;

        GameObject newProjectile = Instantiate(throwingProjectile, transform.position, transform.rotation);

        if (hasMelee) { meleeWeapon.RemoveWeapon(); projectileSprite = gunSprite; newProjectile.tag = "Melee"; }
        else { rangeWeapon.RemoveWeapon(); projectileSprite = rangeSprite; newProjectile.tag = "Range"; }

        newProjectile.GetComponent<SpriteRenderer>().sprite = projectileSprite;

        newProjectile.GetComponent<Rigidbody2D>().AddForce(followMouse.MousePosition() * throwPower);
    }

    public GameObject RangedObject()
    {
        return weaponObject;
    }

    public bool HasMelee()
    {
        return hasMelee;
    }

    public bool HasWeapon()
    {
        return hasWeapon;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(meleePosition, 0.1f);
        Gizmos.DrawSphere(rangePosition, 0.1f);
    }
}
