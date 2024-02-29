using UnityEngine;

public class EnemyAttackCollider : MonoBehaviour
{
    [SerializeField] Animator animator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("Attack");
            other.GetComponent<PlayerMovement>().Death();
        }
    }
}
