using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    public bool canAttack = false;
    public CircleCollider2D range;
    private void OnTriggerSaty2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        { 
          canAttack = true;
        }
    }
}
