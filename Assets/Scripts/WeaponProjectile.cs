using UnityEngine;

public class WeaponProjectile : MonoBehaviour
{
    [SerializeField] float spinSpeed = 40;

    [Header("Collider Height/Length")]
    [SerializeField] float colliderHeight = 1;
    [SerializeField] float colliderWidth = 1;

    Rigidbody2D rigidbody;

    bool midAir = true;

    void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (midAir)
        {
            transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        midAir = false;
        rigidbody.velocity = Vector3.zero;
    }
}
