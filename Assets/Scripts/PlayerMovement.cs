using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 12f;
    [SerializeField] float chargingSpeed = 18f;

    [SerializeField] GameObject startPoint;
   
    [SerializeField] GameObject dialogManger;

    [SerializeField] DialogueManager dialogueManager;

    public bool Dead { get; private set; }

    float horizontal;
    float vertical;
    float originalSpeed;
    bool isCharging = false;
    bool once = true;

    Vector2 movementVector;

    Rigidbody2D myRigidbody;
    Animator myAnimator;
    Extraction extraction;
    NewWeaponManager newWeaponManager;
    Keeper keeper;

    public bool IsWalking { get; private set; } = false;
    public bool IsOpeningAnim { get; private set; } = false;

    void OnEnable()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = transform.GetChild(0).transform.GetChild(1).GetComponent<Animator>();
        extraction = FindObjectOfType<Extraction>();
        originalSpeed = movementSpeed;
        newWeaponManager = FindObjectOfType<NewWeaponManager>();

        keeper = GameObject.FindGameObjectWithTag("Keeper").GetComponent<Keeper>();

        if (!keeper.PlayOpeningAnimation)
        {
            GetComponent<Animator>().enabled = false;

            if (keeper.GrantCheckpoint)
            {
                transform.position = keeper.Checkpoint;
            }
            else
            {
                transform.position = startPoint.transform.position;
            }
        }
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if (keeper.LevelEnded || Dead) return;
        Move();
    }

    void Move()
    {
        if (movementVector.magnitude <= Mathf.Epsilon && !IsOpeningAnim)
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

        myRigidbody.velocity = new Vector2(movementVector.x, movementVector.y).normalized * (movementSpeed * Time.timeScale);
    }

    public void Death()
    {
        if (once)
        {
            once = false;
            myRigidbody.isKinematic = true;
            Dead = true;
            myRigidbody.velocity = Vector3.zero;
            newWeaponManager.SetDeadAnimator();
            FindObjectOfType<Extraction>().DeathScreen();

            /*
            if (GameObject.Find("HellModeManager") != null)
            {
                FindObjectOfType<HellMode>().FillOpacity();
            }
            */
        }
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

    public void CinamaticWalk()
    {
        myAnimator.SetBool("isWalking", true);
        IsOpeningAnim = true;
    }

    public void CinamaticWalkFalse()
    {
        myAnimator.SetBool("isWalking", false);
        IsOpeningAnim = false;

        Animator animator = gameObject.GetComponent<Animator>();
        animator.enabled = false;
    }

    public void StartDialogWithPope()
    {
        dialogManger.SetActive(true);   
    }

}
