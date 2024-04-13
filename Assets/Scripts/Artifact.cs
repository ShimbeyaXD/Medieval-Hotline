using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class Artifact : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] float pickupDistance = 1.0f;

    [Header("Artifact Attributes")]
    [SerializeField] LayerMask artifactLayer;
    [SerializeField] Image artifactImage;
    [SerializeField] Sprite artifactSprite;

    [Header("DialogueSystem")]
    [SerializeField] GameObject dialogueManager;

    PlayerMovement playerMovement;
    ObjectiveUI objectiveUI;

    public bool LevelCleared { get; private set; }

    private void Start()
    {
        LevelCleared = false;
        artifactImage.enabled = false;
        playerMovement = GetComponent<PlayerMovement>();

        objectiveUI = FindObjectOfType<ObjectiveUI>();
        objectiveUI.ObjectiveImage(artifactSprite);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (playerMovement.Dead) return;

        if (other.gameObject.CompareTag("Artifact"))
        {
            LevelCleared = true;
            artifactImage.enabled = true;
            artifactImage.sprite = artifactSprite;
            dialogueManager.SetActive(true);

            other.gameObject.SetActive(false);
        }
    }

}
