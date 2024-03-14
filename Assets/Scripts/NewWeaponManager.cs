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

    [Header("Weapons")]
    [SerializeField] Sprite sword;
    [SerializeField] Sprite axe;
    [SerializeField] Sprite crossBow;
    [SerializeField] Sprite holyCross;
    [SerializeField] Sprite glock;

    [Header("Weapon Icons")]
    [SerializeField] Sprite swordImage;
    [SerializeField] Sprite axeImage;
    [SerializeField] Sprite crossBowImage;
    [SerializeField] Sprite holyCrossImage;
    [SerializeField] Sprite glockImage;

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
    
    public bool AnyWeapon { get; private set; }

    public bool HasWeapon { get; private set; }

    public bool HasCrossbow { get; private set; }

    public bool HasGlock { get; private set; }

    public int Bloon { get; private set; }

    void Start()
    {
        weaponImage.enabled = false;
        followMouse = FindObjectOfType<FollowMouse>();
        attack = GetComponent<Attack>();
    }

    void Update()
    {
        if (HasCrossbow || HasGlock || HasWeapon) AnyWeapon = true;

        if (Input.GetMouseButton(1) && AnyWeapon) WeaponThrow();
        if (Input.GetKeyDown(KeyCode.E) && !AnyWeapon)
        {
            WeaponPickup();
        }
    }

    void WeaponPickup()
    {
        RaycastHit2D ray = Physics2D.BoxCast(transform.position, new Vector2(3, 3), 0, Vector2.up, pickupDistance, gunLayer);

        if (ray.collider == null) return;

        switch (ray.collider.tag)
        {
            case "Sword":
                torsoAnimator.SetBool("Sword", true);

                weaponSprite = sword;
                //ChangeSprite(ray.collider.gameObject);

                weaponImage.enabled = true;
                weaponImage.sprite = swordImage; 

                HasWeapon = true;
                projectileTag = "Sword";

                Destroy(ray.collider.gameObject);
                break;

            case "Axe":
                torsoAnimator.SetBool("Axe", true);

                weaponSprite = axe;
                //ChangeSprite(ray.collider.gameObject);

                weaponImage.enabled = true;
                weaponImage.sprite = axeImage;
                HasWeapon = true;
                projectileTag = "Axe";

                Destroy(ray.collider.gameObject);
                break;

            case "CrossBow":
                torsoAnimator.SetBool("Crossbow", true);

                weaponSprite = crossBow;
                //ChangeSprite(ray.collider.gameObject);

                weaponImage.enabled = true;
                weaponImage.sprite = crossBowImage;
                HasCrossbow = true;
                projectileTag = "CrossBow";

                Destroy(ray.collider.gameObject);
                break;

            case "Cross":
                torsoAnimator.SetBool("Cross", true);

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

        torsoAnimator.SetBool("Sword", false);
        torsoAnimator.SetBool("Axe", false);
        torsoAnimator.SetBool("Cross", false);
        torsoAnimator.SetBool("Crossbow", false);
        torsoAnimator.SetBool("Glock", false);

        HasWeapon = false;
        HasCrossbow = false;
        HasGlock = false;
        AnyWeapon = false;

        weaponImage.sprite = null;
        weaponImage.enabled = false;
        weaponRenderer.sprite = null;

        GameObject newProjectile = Instantiate(throwingProjectile, transform.position, transform.rotation);

        newProjectile.transform.GetChild(0).gameObject.tag = projectileTag;
        newProjectile.GetComponentInChildren<SpriteRenderer>().sprite = weaponSprite;
        newProjectile.GetComponent<WeaponProjectile>().Velocity(throwPower);
        //newProjectile.gameObject.layer = projectileLayer;
        //newProjectile.GetComponent<Rigidbody2D>().AddForce(followMouse.MousePosition() * throwPower);

        Debug.Log("glock threwn");
    }

    public void SetAttackAnimator()
    {
        torsoAnimator.SetTrigger("Attack");
    }

    public void SetPunchAnimator()
    {
        torsoAnimator.SetTrigger("Punch");
    }

    public void SetChargingAnimator()
    {
        torsoAnimator.SetTrigger("Charge");
    }

    public void Glock()
    {
        attack.ResetArrows();
        torsoAnimator.SetBool("Sword", false);
        torsoAnimator.SetBool("Axe", false);
        torsoAnimator.SetBool("Cross", false);
        torsoAnimator.SetBool("Crossbow", false);
        torsoAnimator.SetBool("Glock", true);

        HasGlock = true;
        HasWeapon = false;
        HasCrossbow = false;

        projectileTag = "Glock";
        weaponSprite = glock;
        weaponImage.sprite = glockImage;
    }
}
