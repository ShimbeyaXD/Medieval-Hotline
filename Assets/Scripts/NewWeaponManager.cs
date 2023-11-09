using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class NewWeaponManager : MonoBehaviour
{
    [SerializeField] float pickupDistance = 1;
    [SerializeField] float throwPower = 100;
    [SerializeField] LayerMask gunLayer;
    [SerializeField] GameObject throwingProjectile;

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
        if (Input.GetKeyDown(KeyCode.E) && !HasWeapon) WeaponPickup();
    }

    void WeaponPickup()
    {
        RaycastHit2D ray = Physics2D.BoxCast(transform.position, new Vector2(2, 2), 0, Vector2.up, pickupDistance, gunLayer);

        if (ray.collider == null) return;

        Debug.Log("hit something");

        switch (ray.collider.tag)
        {
            case "Sword":
                weaponSprite = sword;

                HasWeapon = true;

                torsoAnimator.SetBool("HoldingSword", true);

                ChangeSprite();

                Debug.Log(ray.collider.name);
                break;

            case "Axe":
                weaponSprite = axe;

                HasWeapon = true;

                ChangeSprite();

                Debug.Log(ray.collider.name);
                break;

            case "CrossBow":
                weaponSprite = crossBow;

                HasWeapon = true;

                HasCrossbow = true;

                ChangeSprite();

                Debug.Log(ray.collider.name);
                break;

            case "Cross":
                weaponSprite = holyCross;

                HasWeapon = true;

                ChangeSprite();

                Debug.Log(ray.collider.name);
                break;

            default:
                Debug.Log("something went wrong");
                break;
        }
    }

    void ChangeSprite()
    {
        weaponRenderer.sprite = weaponSprite;
    }

    void WeaponThrow()
    {
        if (HasCrossbow) attack.CurrentArrows = 5; 

        HasWeapon = false;
        HasCrossbow = false;
        weaponRenderer.sprite = null;
        

        GameObject newProjectile = Instantiate(throwingProjectile, transform.position, transform.rotation);

        newProjectile.GetComponent<SpriteRenderer>().sprite = weaponSprite;

        newProjectile.GetComponent<Rigidbody2D>().AddForce(followMouse.MousePosition() * throwPower);
    }
}
