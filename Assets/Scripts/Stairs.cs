using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Stairs : MonoBehaviour
{
    [SerializeField] Transform pos;
    
    bool walkingStair = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (walkingStair) { return; }
        walkingStair = true;
        collision.transform.position = pos.position;

        Camera main = FindObjectOfType<Camera>();

        main.transform.position = pos.position;

        StartCoroutine(StairCooldown());
    }

    IEnumerator StairCooldown() {

        pos.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        pos.gameObject.SetActive(true);
        walkingStair = false;
        
        
    }
}
