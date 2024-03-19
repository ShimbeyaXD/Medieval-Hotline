using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    Vector2 mousePosition;
    Vector2 playerPosition;

    public bool FreezeMouse { get; set; } = false;

    private void Start()
    {
        playerPosition = FindObjectOfType<PlayerMovement>().transform.position;
    }

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = new Vector2(mousePosition.x, mousePosition.y);
    }

    public Vector2 MousePosition()
    {
        return mousePosition;
    }
}
