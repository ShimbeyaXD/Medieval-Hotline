using System.Collections;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    Vector2 mousePosition;
    Vector2 playerPosition;

    public bool FreezeMouse { get; set; } = false;

    private void Start()
    {
        StartCoroutine(LookForPlayer());
    }

    IEnumerator LookForPlayer() 
    {
        GameObject player = null;

        while (player == null || !player.activeSelf) 
        {
            player = GameObject.Find("Player");
            yield return new WaitForSeconds(1f);
        }

        playerPosition = player.transform.position;
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
