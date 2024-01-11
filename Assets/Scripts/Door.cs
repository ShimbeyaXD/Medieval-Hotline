using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] float triggerDistance;
    [SerializeField] LayerMask playerLayer;

    BoxCollider2D boxCollider;
    Animator animator;

    void Start()
    {
        boxCollider = transform.GetChild(0).GetComponentInChildren<BoxCollider2D>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, triggerDistance, playerLayer) && Input.GetKeyDown(KeyCode.E))
        {
            boxCollider.isTrigger = true;
            animator.SetBool("Open", true);
        }
    }
}
