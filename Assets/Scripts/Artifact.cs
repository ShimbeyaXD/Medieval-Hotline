using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    [Header("Hellmode Reference")]
    [SerializeField] GameObject hellModeManager;

    PlayerMovement playerMovement;
    ObjectiveUI objectiveUI;

    Keeper keeper;

    bool once = true;

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
            keeper.IsLevelCleared = true;
            other.gameObject.SetActive(false);

            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                dialogueManager.SetActive(true);
            }

            if (SceneManager.GetActiveScene().buildIndex != 2)
            {
                hellModeManager.gameObject.SetActive(true);
            }

        }
        if (other.gameObject.CompareTag("Checkpoint") && once)
        {
            Debug.Log("FOund checkpoiunt");
            once = false;
            Checkpoint(other.gameObject);
        }
    }

    void Checkpoint(GameObject other)
    {
        keeper.RecieveCheckpoint(other.transform.position);

    }

}
