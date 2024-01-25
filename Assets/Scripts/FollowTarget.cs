using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class FollowTarget : MonoBehaviour
{
    Animator cameraAnimator;

    public Transform target; 
    public float smoothSpeed = 0.125f;
    public float maxValue = 1;
    public Vector3 offset;

    bool followCamera = true;

    public bool IsShaking { get; private set; }

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
        if (!IsShaking) { StartCoroutine(Shake(direction, magnitude)); }        
    }

    public IEnumerator Shake(float direction, float magnitude)
    {
        followCamera = false;
        IsShaking = true;

        Vector3 originalPos = transform.position;

        float elapsedTime = 0f;

        while (elapsedTime < direction)
        {
            float xOffset = Random.Range(-0.5f, 0.5f) * magnitude;
            float yOffset = Random.Range(-0.5f, 0.5f) * magnitude;

            xOffset = Mathf.Clamp(xOffset, -maxValue, maxValue);
            yOffset = Mathf.Clamp(yOffset, -maxValue, maxValue);

            transform.localPosition += new Vector3(xOffset, yOffset, 0);

            elapsedTime += Time.deltaTime;

            IsShaking = false;
            yield return null;
        }

        transform.localPosition = originalPos;

        followCamera = true;
    }
}

