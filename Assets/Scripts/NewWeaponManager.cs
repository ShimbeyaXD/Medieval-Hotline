using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NewWeaponManager : MonoBehaviour
{
    [Header("General Weapon Parameters")]
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
    PlayerMovement playerMovement;
    GameObject player;
    
    public bool AnyWeapon { get; private set; }

    public bool HasWeapon { get; private set; }

    public bool HasCrossbow { get; private set; }

    public bool HasGlock { get; private set; }

    public int Bloon { get; private set; }

    void Start()
    {
        StartCoroutine(LookForPlayer());

        weaponImage.enabled = false;


    }

    IEnumerator LookForPlayer()
    {
        while (player == null || !player.gameObject.activeSelf)
        {
            GameObject playerObject = GameObject.Find("Player");

            // Check if playerObject is not null before accessing its transform
            if (playerObject != null)
            {
                player = playerObject;
            }

            yield return new WaitForSeconds(1f);
        }

        followMouse = FindObjectOfType<FollowMouse>();
        attack = GetComponent<Attack>();
        playerMovement = FindObjectOfType<PlayerMovement>();

    }

    void Update()
    {
        if (player.activeSelf == false || playerMovement == null) { return; }
        if (playerMovement.Dead) return;

        if (HasCrossbow || HasGlock || HasWeapon) AnyWeapon = true;

        if (Input.GetButton("Fire2") && AnyWeapon) WeaponThrow(); // Throw
        if (Input.GetButtonDown("Submit") && !AnyWeapon && player.activeSelf) // Pickup
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

                attack.ResetArrows();
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
        newProjectile.transform.localScale = new Vector2(0.75f, 0.75f);
        //newProjectile.gameObject.layer = projectileLayer;
        //newProjectile.GetComponent<Rigidbody2D>().AddForce(followMouse.MousePosition() * throwPower);

        Debug.Log("glock threwn");
    }

    public void SetDeadAnimator()
    {
        legAnimator.SetBool("isWalking", false);
        torsoAnimator.SetTrigger("Dead");
    }

    public void SetAttackAnimator()
    {
        torsoAnimator.SetTrigger("Attack");
    }

    public void SetPunchAnimator()
    {
        torsoAnimator.SetTrigger("Punch");
    }

    public void SetChargingAnimator(bool state)
    {   
        torsoAnimator.SetBool("Charge", state);
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
