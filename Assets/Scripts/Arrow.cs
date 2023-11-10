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
        Debug.Log("Arrow hit");
        rigidbody.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("Hit");
    }
}
