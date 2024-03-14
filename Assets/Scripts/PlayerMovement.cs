using System.Net.NetworkInformation;
using System.Security.Cryptography;
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

    Rigidbody2D rigidbody;
    Animator myAnimator;
    Extraction extraction;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
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
        }
        else
        {
            myAnimator.SetBool("isWalking", true);
            FindObjectOfType<SFXManager>().RunningSFX(true);
        }


        if (isCharging)
        {
            rigidbody.velocity = new Vector2(movementVector.x, movementVector.y).normalized * chargingSpeed;
            return;
        }

        movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        rigidbody.velocity = new Vector2(movementVector.x, movementVector.y).normalized * movementSpeed;
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
