using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Extraction : MonoBehaviour
{
    [SerializeField] LayerMask exitLayer;
    [SerializeField] float detectionDistance = 2;
    [SerializeField] GameObject continueButton;

    Artifact artifact;
    RoundTimer roundTimer;
    PowerManager powerManager;

    void Start()
    {
        continueButton.gameObject.SetActive(false);

        roundTimer = FindObjectOfType<RoundTimer>();
        powerManager = FindObjectOfType<PowerManager>();
        artifact = FindAnyObjectByType<Artifact>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && artifact.LevelCleared)
        {
            RaycastHit2D ray = Physics2D.BoxCast(transform.position, new Vector2(2, 2), 0, Vector2.up, detectionDistance, exitLayer);

            if (ray.collider != null)
            {
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                Debug.Log("Change scene");
                continueButton.gameObject.SetActive(true);
                powerManager.ShowKillText();

                // Change scene
            }
        }
    }
}
