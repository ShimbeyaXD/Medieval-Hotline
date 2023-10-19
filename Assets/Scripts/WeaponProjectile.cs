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
        else
        {
            transform.rotation = Quaternion.Euler(0,0,0); 
            rigidbody.velocity = Vector3.zero;
            Debug.Log("freeze");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("hit");
        if (gameObject.tag == "Range")
        {
            Debug.Log("should destroy ranged object");
            Destroy(gameObject);
        }
        else
        {
            midAir = false;
            rigidbody.isKinematic = true;
        }
    }
}
