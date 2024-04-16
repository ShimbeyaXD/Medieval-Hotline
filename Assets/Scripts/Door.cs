using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] float triggerDistance;
    [SerializeField] LayerMask targetLayer;
    
    BoxCollider2D boxCollider;
    Animator animator;

    Keeper keeper;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        boxCollider = transform.GetChild(0).GetComponentInChildren<BoxCollider2D>();
        
        keeper = GameObject.FindGameObjectWithTag("Keeper").GetComponent<Keeper>();
        if (keeper.SearchAndDestroy(gameObject)) Destroy(gameObject); 
    }

    void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, triggerDistance, targetLayer) && Input.GetButtonDown("Submit") && !animator.GetBool("Open"))
        {
            SoundManager.PlaySound("OppenDoor");

            boxCollider.isTrigger = true;
            animator.SetBool("Open", true);

            keeper.DoorInstance(GetComponent<Door>(), gameObject);
        }
    }

    public void Replace()
    {
        Destroy(gameObject);
    }
}
