using UnityEngine;
using System.Collections;

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
        StartCoroutine(LookForTarget());
        animator = transform.parent.GetChild(0).GetComponent<Animator>();
        enemyYes = transform.parent.GetComponent<EnemyYEs>();
        enemyBehaviour = FindObjectOfType<EnemyBehavior>();
        newWeaponManager = FindObjectOfType<NewWeaponManager>();
        followTarget = FindObjectOfType<FollowTarget>();
    }

    IEnumerator LookForTarget()
    {
        while (playerAttack == null || !playerAttack.gameObject.activeSelf)
        {
            GameObject playerObject = GameObject.Find("Player");

            if (playerObject != null)
            {
                playerAttack = playerObject.GetComponent<Attack>();
                yield break;
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !playerAttack.PlayerIsCharging)
        {
            SoundManager.PlaySound("DemonAttck");
            if (enemyYes.ReturnDemonType())
            {
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
        followTarget.StartShake(0.2f, 0.7f);
        enemyBehaviour.StopFollowing();
    }
}
