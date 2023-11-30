using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float detectionAndSightRange = 5f;

    NavMeshAgent agent;
    Animator anim;

    bool isChasingTarget = false;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (!isChasingTarget && TargetInDetectionRadius())
        {
            StartCoroutine(ChaseTargetRoutine());
        }

        UpdateAnimation();
    }

    private bool TargetInDetectionRadius()
    {
        Vector2 directionToTarget = (target.position - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToTarget, detectionAndSightRange);

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
        agent.SetDestination(target.position);
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

        agent.isStopped = true;
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
