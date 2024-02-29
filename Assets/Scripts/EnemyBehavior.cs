using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float detectionAndSightRange = 5f;
    [SerializeField] float knockbackCooldown = 3;

    NavMeshAgent agent;
    Animator anim;
    EnemyYEs enemyYes;

    public bool isChasingTarget { get; private set; }

    void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
        enemyYes = GetComponent<EnemyYEs>();

        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        Debug.Log("update pos is "+ agent.updatePosition);

        if (target == null) { return; }

        if (enemyYes.Punched)
        {
            StopCoroutine(ChaseTargetRoutine());
            agent.updatePosition = false;
            isChasingTarget = false;
            Debug.Log("I am punched");
        }

        if (!isChasingTarget && TargetInDetectionRadius() && !enemyYes.Punched)
        {
            agent.updatePosition = true;
            StartCoroutine(ChaseTargetRoutine());
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

        // alex var här
        agent.updatePosition = false;
        isChasingTarget = false;
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
}
