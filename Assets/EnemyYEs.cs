using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyYEs : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
       transform.position = Vector2.MoveTowards(gameObject.transform.position, player.position, speed * Time.deltaTime);
        Look();
    }

    void Look()
    {
        Vector3 lookAt = player.position;

        float AngleRad = Mathf.Atan2(lookAt.y - this.transform.position.y, lookAt.x - this.transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;

        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
    }

    public void TakeDamage() 
    {
        FindObjectOfType<PowerManager>().addHoliness(20f);
        Death();
    }

    private void Death()
    {
        gameObject.SetActive(false);                                                    
                                                                                                                        Application.Quit();
    }
}
