using UnityEngine;
using UnityEngine.UI;

public class NewWeaponManager : MonoBehaviour
{
    [SerializeField] float pickupDistance = 1;
    [SerializeField] float throwPower = 100;
    [SerializeField] LayerMask gunLayer;
    [SerializeField] GameObject throwingProjectile;
    [SerializeField] Image weaponImage;
    [SerializeField] Sprite holdingWeaponSprite;

    [Header("Character Sprites")]
    [SerializeField] Sprite characterIdle;
    [SerializeField] Sprite characterSword;
    [SerializeField] Sprite characterAxe;
    [SerializeField] Sprite characterCrossBow;
    [SerializeField] Sprite characterHolyCross;

    [Header("Weapons")]
    [SerializeField] Sprite sword;
    [SerializeField] Sprite axe;
    [SerializeField] Sprite crossBow;
    [SerializeField] Sprite holyCross;

    [Header("Weapon Icons")]
    [SerializeField] Sprite swordImage;
    [SerializeField] Sprite axeImage;
    [SerializeField] Sprite crossBowImage;
    [SerializeField] Sprite holyCrossImage;

    [Header("Animators")]
    [SerializeField] Animator torsoAnimator;
    [SerializeField] Animator legAnimator;
    [SerializeField] Animator weaponAnimator;
    [SerializeField] SpriteRenderer weaponRenderer;
    [SerializeField] SpriteRenderer torsoRenderer;

    string projectileTag;

    Sprite weaponSprite;
    FollowMouse followMouse;
    Animator myAnimator;
    Attack attack;

    public bool HasWeapon { get; private set; }

    public bool HasCrossbow { get; private set; }

    public int Bloon { get; private set; }

    void Start()
    {
        followMouse = FindObjectOfType<FollowMouse>();
        attack = GetComponent<Attack>();
    }

    void Update()
    {
        if (Input.GetMouseButton(1) && HasWeapon) WeaponThrow();
        if (Input.GetKeyDown(KeyCode.E) && !HasWeapon)
        {
            WeaponPickup();
        }
    }

    void WeaponPickup()
    {
        RaycastHit2D ray = Physics2D.BoxCast(transform.position, new Vector2(2, 2), 0, Vector2.up, pickupDistance, gunLayer);

        if (ray.collider == null) return;

        switch (ray.collider.tag)
        {
            case "Sword":
                torsoRenderer.sprite = characterSword;

                weaponSprite = sword;
                //ChangeSprite(ray.collider.gameObject);

                weaponImage.enabled = true;
                weaponImage.sprite = swordImage; 

                HasWeapon = true;
                projectileTag = "Sword";

                Destroy(ray.collider.gameObject);
                break;

            case "Axe":
                torsoRenderer.sprite = characterAxe;

                weaponSprite = axe;
                //ChangeSprite(ray.collider.gameObject);

                weaponImage.enabled = true;
                weaponImage.sprite = axeImage;
                HasWeapon = true;
                projectileTag = "Axe";

                Destroy(ray.collider.gameObject);
                break;

            case "CrossBow":
                torsoRenderer.sprite = characterCrossBow;

                weaponSprite = crossBow;
                //ChangeSprite(ray.collider.gameObject);

                weaponImage.enabled = true;
                weaponImage.sprite = crossBowImage;
                HasWeapon = true;
                HasCrossbow = true;
                projectileTag = "CrossBow";

                Destroy(ray.collider.gameObject);
                break;

            case "Cross":
                torsoRenderer.sprite = characterHolyCross;

                weaponSprite = holyCross;
                //ChangeSprite(ray.collider.gameObject);

                weaponImage.enabled = true;
                weaponImage.sprite = holyCrossImage;
                HasWeapon = true;
                projectileTag = "Cross";

                Destroy(ray.collider.gameObject);
                break;

            default:
                Debug.Log("something went wrong");
                break;
        }
    }

    void WeaponThrow()
    {
        if (HasCrossbow) attack.CurrentArrows = 5;

        torsoRenderer.sprite = characterIdle;
        HasWeapon = false;
        HasCrossbow = false;
        weaponImage.sprite = null;
        weaponImage.enabled = false;
        weaponRenderer.sprite = null;
        

        GameObject newProjectile = Instantiate(throwingProjectile, transform.position, transform.rotation);

        newProjectile.GetComponent<SpriteRenderer>().sprite = weaponSprite;
        newProjectile.gameObject.tag = projectileTag;
        //newProjectile.gameObject.layer = projectileLayer;
        //newProjectile.GetComponent<Rigidbody2D>().AddForce(followMouse.MousePosition() * throwPower);
        newProjectile.GetComponent<WeaponProjectile>().Velocity(throwPower);
    }

    public void SetAttackAnimator()
    {
        torsoAnimator.SetTrigger("Attack");
    }
}
