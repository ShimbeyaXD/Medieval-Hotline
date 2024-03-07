using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float detectionAndSightRange = 5f;

    [Header("Knockback Attributes")]
    [SerializeField] float knockbackCooldown = 3;
    [SerializeField] float knockbackForce = 3;
    [SerializeField] LayerMask wallLayer;

    NavMeshAgent agent;
    Animator anim;
    EnemyYEs enemyYes;
    Rigidbody2D myRigidbody;

    bool once = true;

    public bool isChasingTarget { get; private set; }

    void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
        enemyYes = GetComponent<EnemyYEs>();
        myRigidbody = GetComponent<Rigidbody2D>();

        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {

        Debug.Log("update pos is "+ agent.updatePosition);
        Debug.Log("Enemypunched is " + enemyYes.Punched);

        if (target == null) { return; }

        if (enemyYes.Punched)
        {
            StopCoroutine(ChaseTargetRoutine());

            ApplyKnockback();
        }

        if (!isChasingTarget && TargetInDetectionRadius() && !enemyYes.Punched)
        {
            myRigidbody.freezeRotation = false;
            myRigidbody.velocity = Vector3.zero;
            StartCoroutine(ChaseTargetRoutine());
            agent.updatePosition = true;
            agent.updateRotation = true;
            once = true;
        }

        UpdateAnimation();
    }

    private bool TargetInDetectionRadius()
    {
        Vector2 directionToTarget = (target.position - transform.position).normalized;

        int layerMask = ~(1 << gameObject.layer);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToTarget, detectionAndSightRange, layerMask);

        if (hit.collider != null)
        {
            Debug.DrawLine(transform.position, hit.point, Color.blue);

            if (hit.collider.gameObject.tag == "Player")
            {
                Debug.Log("SEEN");
                return true;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, directionToTarget * detectionAndSightRange, Color.blue);
        }

        return false;
    }

    private void FollowTarget()
    {
        agent.updatePosition = true;
        if (!enemyYes.Punched) { agent.SetDestination(target.position); } 
    }

    IEnumerator ChaseTargetRoutine()
    {
        Debug.Log("CHASE");
        isChasingTarget = true;
        agent.ResetPath();

        float timeSinceStarted = 0f;

        while (timeSinceStarted < 7f)
        {
            FollowTarget();
            UpdateRotation();

            yield return null;
            timeSinceStarted += Time.deltaTime;
        }

        // alex var h�r
        agent.updatePosition = false;
        isChasingTarget = false;


        //myRigidbody.AddForce(knockbackDirection.normalized * 100000);
    }

    private void UpdateRotation()
    {
        Vector3 desiredMoveDirection = agent.velocity.normalized;

        if (desiredMoveDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(desiredMoveDirection.y, desiredMoveDirection.x) * Mathf.Rad2Deg;
            Quaternion newRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10f);
        }
    }

    private void UpdateAnimation()
    {
        //animation
    }

    public void ApplyKnockback()
    {
        if (once)
        {
            myRigidbody.freezeRotation = true;
            agent.isStopped = true; 
            agent.updatePosition = false;
            agent.updateRotation = false;
            isChasingTarget = false;

            Vector2 knockbackDirection = target.position - transform.position;
            myRigidbody.velocity = Vector3.zero;

            myRigidbody.velocity = -knockbackDirection.normalized * knockbackForce;
            Debug.Log("called knock");
            once = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == wallLayer)
        {
            myRigidbody.velocity = Vector3.zero;
            Debug.Log("Hit a wall");
        }
    }
}
