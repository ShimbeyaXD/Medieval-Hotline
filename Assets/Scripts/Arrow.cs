using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] bool isEnemyArrow;

    bool hasHit = false;

    Rigidbody2D rigidbody;
    Animator animator;

    void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!hasHit && other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyYEs>().TakeDamage();
            Destroy(gameObject);
        }
        if (!hasHit && other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().Death();
            Destroy(gameObject);
        }

        //Debug.Log("Arrow hit " + other.collider.name);
        animator.SetTrigger("Hit");
        hasHit = true;
        rigidbody.bodyType = RigidbodyType2D.Static;
    }
}
