using UnityEngine;
using UnityEngine.UI;

public class Artifact : MonoBehaviour
{
    [SerializeField] float pickupDistance = 1.0f;
    [SerializeField] LayerMask artifactLayer;
    [SerializeField] Image artifactImage;
    [SerializeField] Sprite artifactSprite;

    public bool LevelCleared { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Artifact"))
        {
            PickupArtifact();
            other.gameObject.SetActive(false);
        }
    }

    void PickupArtifact()
    {
        LevelCleared = true;
        artifactImage.sprite = artifactSprite;
    }
}
