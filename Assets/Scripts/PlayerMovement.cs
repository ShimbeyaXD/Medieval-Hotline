using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 3.5f;

    float horizontal;
    float vertical;

    Rigidbody2D rigidbody;
    Animator myAnimator;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        myAnimator = transform.GetChild(0).transform.GetChild(1).GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Debug.Log(myAnimator);


        transform.position += new Vector3(horizontal * movementSpeed, vertical * movementSpeed, 0);
    }

    void Move()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        Vector2 movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (movementVector.magnitude <= Mathf.Epsilon)
        {
            myAnimator.SetBool("isWalking", false);
            Debug.Log("not walking");
        }
        else
        {
            myAnimator.SetBool("isWalking", true);
            Debug.Log("is walking");
        }
    }
}
