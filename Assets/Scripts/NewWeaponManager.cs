using UnityEngine;
using UnityEngine.UI;

public class NewWeaponManager : MonoBehaviour
{
    [SerializeField] float pickupDistance = 1;
    [SerializeField] float throwPower = 100;
    [SerializeField] LayerMask gunLayer;
    [SerializeField] GameObject throwingProjectile;
    [SerializeField] Image weaponImage;

    [Header("Weapons")]
    [SerializeField] Sprite sword;
    [SerializeField] Sprite axe;
    [SerializeField] Sprite crossBow;
    [SerializeField] Sprite holyCross;

    [Header("Animators")]
    [SerializeField] Animator torsoAnimator;
    [SerializeField] Animator legAnimator;
    [SerializeField] Animator weaponAnimator;
    [SerializeField] SpriteRenderer weaponRenderer;

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

            // aloha
        }
    }

    void WeaponPickup()
    {
        RaycastHit2D ray = Physics2D.BoxCast(transform.position, new Vector2(2, 2), 0, Vector2.up, pickupDistance, gunLayer);

        if (ray.collider == null) return;

        switch (ray.collider.tag)
        {
            case "Sword":
                weaponSprite = sword;
                weaponImage.sprite = sword;
                HasWeapon = true;
                projectileTag = "Sword";

                //torsoAnimator.SetBool("HoldingMelee", true);

                ChangeSprite(ray.collider.gameObject);

                Debug.Log(ray.collider.name);
                break;

            case "Axe":
                weaponSprite = axe;
                weaponImage.sprite = axe;
                HasWeapon = true;
                projectileTag = "Axe";

                //torsoAnimator.SetBool("HoldingMelee", true);

                ChangeSprite(ray.collider.gameObject);

                Debug.Log(ray.collider.name);
                break;

            case "CrossBow": 
                weaponSprite = crossBow;
                weaponImage.sprite = crossBow;
                HasWeapon = true;
                HasCrossbow = true;
                projectileTag = "CrossBow";

                ChangeSprite(ray.collider.gameObject);

                Debug.Log(ray.collider.name);
                break;

            case "Cross":
                weaponSprite = holyCross;
                weaponImage.sprite = holyCross;
                HasWeapon = true;
                projectileTag = "Cross";

                //torsoAnimator.SetBool("HoldingMelee", true);

                ChangeSprite(ray.collider.gameObject);

                Debug.Log(ray.collider.name);
                break;

            default:
                Debug.Log("something went wrong");
                break;
        }
    }

    void ChangeSprite(GameObject weapon)
    {
        weaponRenderer.sprite = weaponSprite;

        Destroy(weapon);
    }

    void WeaponThrow()
    {
        if (HasCrossbow) attack.CurrentArrows = 5;

        torsoAnimator.SetBool("HoldingMelee", false);
        HasWeapon = false;
        HasCrossbow = false;
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
