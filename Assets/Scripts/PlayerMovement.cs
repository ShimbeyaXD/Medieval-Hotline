using System.Security.Cryptography;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 12f;

    float horizontal;
    float vertical;

    Rigidbody2D rigidbody;
    Animator myAnimator;
    Extraction extraction;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        myAnimator = transform.GetChild(0).transform.GetChild(1).GetComponent<Animator>();
        extraction = GetComponent<Extraction>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if (extraction.LevelEnded) return;
        Move();       
    }

    void Move()
    {
        Vector2 movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        rigidbody.velocity = new Vector3(movementVector.x * movementSpeed, movementVector.y * movementSpeed, 0);

        if (movementVector.magnitude <= Mathf.Epsilon)
        {
            myAnimator.SetBool("isWalking", false);
        }
        else
        {
            myAnimator.SetBool("isWalking", true);
        }
    }

    public void Death() 
    { 
      gameObject.SetActive(false);
    }
}
