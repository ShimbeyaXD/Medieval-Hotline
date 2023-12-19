using UnityEngine;

public class WeaponProjectile : MonoBehaviour
{
    [SerializeField] float spinSpeed = 40;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] ParticleSystem destroyParticle;

    [Header("Collider Height/Length")]
    [SerializeField] float colliderHeight = 1;
    [SerializeField] float colliderWidth = 1;

    Rigidbody2D rigidbody;
    FollowMouse followMouse;

    Vector2 playerPosition;
    bool midAir = true;
    int layer;

    void OnEnable()
    {
        layer = LayerMask.NameToLayer("Gun");

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

    public void AssignParticle(ParticleSystem particles)
    {
        destroyParticle = particles;
    }

    public void Velocity(float throwPower)
    {
        rigidbody.AddForce(-transform.right * throwPower);
        midAir = true;

        Debug.Log("THrowpower is " + throwPower);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.gameObject.GetComponent<EnemyYEs>().TakeDamage();
        }

        if (gameObject.tag == "CrossBow")
        {
            destroyParticle.Play();
            Destroy(gameObject);
        }

        else
        {
            midAir = false;
            gameObject.layer = layer;
            rigidbody.bodyType = RigidbodyType2D.Static;
        }
    }
}
