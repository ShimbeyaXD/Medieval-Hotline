using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    Vector2 mousePosition;
    
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
