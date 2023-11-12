using UnityEngine;

public class WeaponProjectile : MonoBehaviour
{
    [SerializeField] float spinSpeed = 40;
    [SerializeField] LayerMask gunLayer;

    [Header("Collider Height/Length")]
    [SerializeField] float colliderHeight = 1;
    [SerializeField] float colliderWidth = 1;

    Rigidbody2D rigidbody;
    FollowMouse followMouse;

    bool midAir = true;

    void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        followMouse = FindObjectOfType<FollowMouse>();
    }

    void Update()
    {
        if (midAir)
        {
            transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
        }
    }

    public void Velocity(float throwPower)
    {
        rigidbody.AddForce(followMouse.MousePosition().normalized * throwPower);
        midAir = true;

        Debug.Log("THrowpower is " + throwPower);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (gameObject.tag == "CrossBow")
        {
            Destroy(gameObject);
        }
        else
        {
            midAir = false;
            gameObject.layer = gunLayer;
            //rigidbody.bodyType = RigidbodyType2D.Static;
        }
    }
}
