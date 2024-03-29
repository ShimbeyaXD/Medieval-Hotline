using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 12f;
    [SerializeField] float chargingSpeed = 18f;

    float horizontal;
    float vertical;
    float originalSpeed;
    bool isCharging = false;

    Vector2 movementVector;

    Rigidbody2D myRigidbody;
    Animator myAnimator;
    Extraction extraction;

    public bool IsWalking { get; private set; } = false;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = transform.GetChild(0).transform.GetChild(1).GetComponent<Animator>();
        extraction = GetComponent<Extraction>();
        originalSpeed = movementSpeed;
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
        if (movementVector.magnitude <= Mathf.Epsilon)
        {
            myAnimator.SetBool("isWalking", false);
            FindObjectOfType<SFXManager>().RunningSFX(false);
            IsWalking = false;
        }
        else
        {
            myAnimator.SetBool("isWalking", true);
            FindObjectOfType<SFXManager>().RunningSFX(true);
            IsWalking = true;
        }


        if (isCharging)
        {
            myRigidbody.velocity = new Vector2(movementVector.x, movementVector.y).normalized * chargingSpeed;
            return;
        }

        movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        myRigidbody.velocity = new Vector2(movementVector.x, movementVector.y).normalized * movementSpeed;
    }

    public void Death() 
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void ChangeSpeed(bool increase)
    {
        if (increase)
        {
            isCharging = true;
            movementSpeed = chargingSpeed;
        }
        else if (!increase)
        {
            isCharging = false;
            movementSpeed = originalSpeed;
        }
    }

}
