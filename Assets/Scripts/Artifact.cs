using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class Artifact : MonoBehaviour
{
    [SerializeField] float pickupDistance = 1.0f;
    [SerializeField] LayerMask artifactLayer;
    [SerializeField] Image artifactImage;
    [SerializeField] Sprite artifactSprite;

    public bool LevelCleared { get; private set; }

    private void Start()
    {
        LevelCleared = false;
        artifactImage.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Artifact"))
        {
            LevelCleared = true;
            artifactImage.enabled = true;
            artifactImage.sprite = artifactSprite;
            other.gameObject.SetActive(false);
        }
    }

}
