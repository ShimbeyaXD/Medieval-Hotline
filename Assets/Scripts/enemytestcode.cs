using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemytestcode : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;
    float maxDistance = 100;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false;
        agent.updateRotation = false;
    }

    void Update()
    {
        if (LookforTarget())
        {
            FollowTarget();
        }
    }

    private bool LookforTarget()
    {
        

        Vector2 direction = (target.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance);

        if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player seen!");
            // You might want to perform actions here when the player is seen
            // For example, set the target as the player
            target = hit.collider.transform;

            return true;
        }
        else
        {
            return false;
        }
    }

    private void FollowTarget()
    {
        if (target == null) return;
        agent.SetDestination(target.position);
    }
}