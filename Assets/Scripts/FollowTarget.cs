using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    Animator cameraAnimator;

    public Transform target; 
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }

    private void Start()
    {
        cameraAnimator = GetComponent<Animator>();
    }

    public void PlaytAnim()
    {
        cameraAnimator.SetTrigger("Jump");
    }
}

