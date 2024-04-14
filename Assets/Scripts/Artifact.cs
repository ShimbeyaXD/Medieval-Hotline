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

    Keeper keeper;

    private void Start()
    {
        keeper = GameObject.FindGameObjectWithTag("Keeper").GetComponent<Keeper>();

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
            artifactImage.enabled = true;
            artifactImage.sprite = artifactSprite;
            dialogueManager.SetActive(true);
            other.gameObject.SetActive(false);

            keeper.IsLevelCleared = true;
        }
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            Checkpoint(other.gameObject);
        }
    }

    void Checkpoint(GameObject other)
    {
        Debug.Log("checkpoint granted");
        keeper.RecieveCheckpoint(other.transform.position);
    }

}
