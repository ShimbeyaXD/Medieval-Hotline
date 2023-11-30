using System.Collections;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    Animator cameraAnimator;

    public Transform target; 
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    bool followCamera = true;

    void FixedUpdate()
    {
        if (target != null && followCamera)
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

    public void PlayAnim()
    {
        cameraAnimator.SetTrigger("Jump");
    }

    public void StartShake(float direction, float magnitude)
    {
        StartCoroutine(Shake(direction, magnitude));
    }

    public IEnumerator Shake(float direction, float magnitude)
    {
        Debug.Log("Camera shake NOW");
        followCamera = false;

        Vector3 originalPos = new Vector3(0, 0, -10);

        float elapsedTime = 0f;

        while (elapsedTime < direction)
        {
            float xOffset = Random.Range(-0.5f, 0.5f) * magnitude;
            float yOffset = Random.Range(-0.5f, 0.5f) * magnitude;

            transform.localPosition = new Vector3(xOffset, yOffset, originalPos.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;

        followCamera = true;
    }
}

