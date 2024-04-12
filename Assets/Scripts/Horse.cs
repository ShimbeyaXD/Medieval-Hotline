using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : MonoBehaviour
{
    [SerializeField] Transform myTransfrom;
    [SerializeField] Transform endPosition;

    [SerializeField] float speed;

    private Vector3 velocity = Vector3.zero;


    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(myTransfrom.position, endPosition.position, ref velocity ,speed * Time.deltaTime, 4f);
    }
}
