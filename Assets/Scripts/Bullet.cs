using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rigidbody;

    void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.gameObject.GetComponent<EnemyYEs>().TakeDamage();
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Debug.Log("Bullet hit");
    }
}
