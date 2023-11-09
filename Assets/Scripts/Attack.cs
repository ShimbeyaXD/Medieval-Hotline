using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject rangeWeapon;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] float shootForce = 4000;
    [SerializeField] int maxArrows = 5;
    
    NewWeaponManager newWeaponManager;
    FollowMouse followMouse;
    public int CurrentArrows { get; set; } = 5;

    void Start()
    {
        boxCollider.enabled = false;
        newWeaponManager = FindObjectOfType<NewWeaponManager>();
        followMouse = FindObjectOfType<FollowMouse>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && newWeaponManager.HasWeapon) 
        {
            if (newWeaponManager.HasCrossbow)
            {
                if (CurrentArrows-- > 0)
                {
                    Debug.Log(CurrentArrows);

                    Vector3 direction = followMouse.MousePosition();

                    float angle = Mathf.Rad2Deg * (Mathf.Atan2(direction.y, direction.x));

                    GameObject newProjectile = Instantiate(arrow, rangeWeapon.transform.position, Quaternion.Euler(0, 0, 270 + angle));

                    newProjectile.GetComponent<Rigidbody2D>().AddForce(direction.normalized * shootForce);
                }
            }
            else { EnableMelee(); }
        }
    }

    public void ResetArrows()
    {
        CurrentArrows = 5;
    }

    public void EnableMelee()
    {
        boxCollider.enabled = true;
    }

    public void DisableMelee() // trigger from animation events
    {
        boxCollider.enabled = false;
    }
}
