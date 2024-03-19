using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    bool lockRotation;

    private void Update()
    {
        if (lockRotation == true) { return; }
        Look();
    }

    void Look()
    {
        Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 lookAt = mouseScreenPosition;

        float AngleRad = Mathf.Atan2(lookAt.y - this.transform.position.y, lookAt.x - this.transform.position.x);

        float AngleDeg = (180 / Mathf.PI) * AngleRad;

        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg + 180);
    }

    public void LockRotation(bool state)
    {
        lockRotation = state;
    }
}
