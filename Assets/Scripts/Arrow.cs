using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rigidbody;
    Animator animator;

    void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.gameObject.GetComponent<EnemyYEs>().TakeDamage();
        }

        Debug.Log("Arrow hit");
        animator.SetTrigger("Hit");
        rigidbody.bodyType = RigidbodyType2D.Static;
    }
}
