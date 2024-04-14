using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : MonoBehaviour
{
    [SerializeField] Transform myTransfrom;
    [SerializeField] Transform endPosition;

    [SerializeField] float speed;

    private Vector3 velocity = Vector3.zero;
    private bool reachedEnd = false;

    // Update is called once per frame
    void Update()
    {
        if (!reachedEnd)
        {
            transform.position = Vector3.SmoothDamp(myTransfrom.position, endPosition.position + new Vector3(0.24f, 0, 0), ref velocity, speed);

            if (Vector3.Distance(myTransfrom.position, endPosition.position + new Vector3(0.24f, 0, 0)) < 0.01f)
            {
                reachedEnd = true;
                endPosition.gameObject.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}
