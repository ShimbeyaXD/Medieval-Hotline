using UnityEngine;
using UnityEngine.UI;

public class Artifact : MonoBehaviour
{
    [SerializeField] float pickupDistance = 1.0f;
    [SerializeField] LayerMask artifactLayer;
    [SerializeField] Image artifactImage;
    [SerializeField] Sprite artifactSprite;

    void WeaponPickup()
    {
        RaycastHit2D ray = Physics2D.BoxCast(transform.position, new Vector2(2, 2), 0, Vector2.up, pickupDistance, artifactLayer);

        if (ray.collider == null) return;

        switch (ray.collider.tag)
        {
            case "Artifact":

                artifactImage.sprite = artifactSprite;

                break;
            default:

                Debug.Log("something went wrong");
                break;
        }
    }

}
