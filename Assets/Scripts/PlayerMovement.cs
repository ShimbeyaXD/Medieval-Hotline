using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 3.5f;

    float horizontal;
    float vertical;

    Rigidbody2D rigidbody2D;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();

        transform.position += new Vector3(horizontal * movementSpeed, vertical * movementSpeed, 0);
    }

    void Move()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }
}
