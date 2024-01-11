using UnityEngine;
using UnityEngine.SceneManagement;

public class Extraction : MonoBehaviour
{
    [SerializeField] LayerMask exitLayer;
    [SerializeField] float detectionDistance = 2;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D ray = Physics2D.BoxCast(transform.position, new Vector2(2, 2), 0, Vector2.up, detectionDistance, exitLayer);

            if (ray.collider != null)
            {
                Debug.Log("Change scene");

                // Change scene
            }
        }
    }
}
