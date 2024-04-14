using System.Collections;
using Unity.Properties;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 12f;
    [SerializeField] float chargingSpeed = 18f;
    [SerializeField] float reloadSceneDelay = 2;

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

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = transform.GetChild(0).transform.GetChild(1).GetComponent<Animator>();
        extraction = GetComponent<Extraction>();
        originalSpeed = movementSpeed;
        newWeaponManager = FindObjectOfType<NewWeaponManager>();
        
        keeper = GameObject.FindGameObjectWithTag("Keeper").GetComponent<Keeper>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if (extraction.LevelEnded || Dead) return;
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
        return;
        if (once)
        {
            once = false;
            myRigidbody.isKinematic = true;
            myRigidbody.velocity = Vector3.zero;
            newWeaponManager.SetDeadAnimator();
            StartCoroutine(ReloadScene());

            if (GameObject.Find("HellModeManager") != null)
            {
                FindObjectOfType<HellMode>().FillOpacity();
            }
        }
    }

    IEnumerator ReloadScene()
    {
        Dead = true;
        yield return new WaitForSeconds(reloadSceneDelay);
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

    public void CinamaticWalk()
    {
        myAnimator.SetBool("isWalking", true);
        IsOpeningAnim = true;
        Debug.Log("Walk did walk");
    }

    public void CinamaticWalkFalse()
    {
        myAnimator.SetBool("isWalking", false);
        IsOpeningAnim = false;

        Debug.Log("Walk finshed");

        Animator animator = gameObject.GetComponent<Animator>();
        animator.enabled = false;
    }

}
