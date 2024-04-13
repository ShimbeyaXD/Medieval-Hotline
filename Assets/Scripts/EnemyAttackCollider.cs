using UnityEngine;

public class EnemyAttackCollider : MonoBehaviour
{
    Animator animator;
    EnemyYEs enemyYes;
    EnemyBehavior enemyBehaviour;
    NewWeaponManager newWeaponManager;
    Attack playerAttack;
    FollowTarget followTarget;

    private void Start()
    {
        animator = transform.parent.GetChild(0).GetComponent<Animator>();
        enemyYes = transform.parent.GetComponent<EnemyYEs>();
        enemyBehaviour = FindObjectOfType<EnemyBehavior>();
        newWeaponManager = FindObjectOfType<NewWeaponManager>();
        playerAttack = FindObjectOfType<Attack>();
        followTarget = FindObjectOfType<FollowTarget>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !playerAttack.PlayerIsCharging)
        {
            if (enemyYes.ReturnDemonType())
            {
                Debug.Log("attacking");

                int randomNum = Random.Range(0, 2);
                if (randomNum == 0)
                {
                    animator.SetTrigger("AttackLeft");
                }
                if (randomNum == 1)
                {
                    animator.SetTrigger("AttackRight");
                }
            }
            else
            {
                animator.SetTrigger("Attack");
            }
            other.GetComponent<PlayerMovement>().Death();
            KillPlayer();
        }
    }

    void KillPlayer()
    {
        followTarget.StartShake(0.2f, 0.5f);
        enemyBehaviour.StopFollowing();
    }
}
