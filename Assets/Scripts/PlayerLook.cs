using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    Attack playerAttack;
    PlayerMovement playerMovement;
    Camera mainCamera;

    private void Start()
    {
        playerAttack = GetComponent<Attack>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (Time.timeScale == 0) { return; }
        if (playerAttack.PlayerIsCharging || playerMovement.Dead) { return; }

        Look();
    }

    void Look()
    {
        Vector3 lookAt;

        // Check if there's mouse input available
        if (Input.mousePresent) // Assuming left mouse button
        {
            // Get the mouse position in world space
            lookAt = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            // Get input from the right joystick of the Xbox controller
            float rightStickX = Input.GetAxis("RightStickX");
            float rightStickY = Input.GetAxis("RightStickY");

            // Calculate the angle based on the right joystick input
            float angleRadJoystick = Mathf.Atan2(rightStickY, rightStickX);
            float angleDegJoystick = Mathf.Rad2Deg * angleRadJoystick;

            // Calculate the position to look at based on the angle
            lookAt = transform.position + new Vector3(Mathf.Cos(angleRadJoystick), Mathf.Sin(angleDegJoystick), 0);
        }

        // Calculate the angle to look at the target position
        float AngleRadMouse = Mathf.Atan2(lookAt.y - this.transform.position.y, lookAt.x - this.transform.position.x);
        float AngleDegMouse = (180 / Mathf.PI) * AngleRadMouse;

        // Rotate the object to face the calculated angle
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDegMouse + 180);
    }
}
