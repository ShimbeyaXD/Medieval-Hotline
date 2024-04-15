using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyYEs>().TakeDamage();
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
