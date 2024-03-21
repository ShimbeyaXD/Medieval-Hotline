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

    public bool LevelEnded { get; private set; }

    void Start()
    {
        if(continueButton == null) { return; }

        continueButton.gameObject.SetActive(false);

        roundTimer = FindObjectOfType<RoundTimer>();
        powerManager = FindObjectOfType<PowerManager>();
        artifact = FindAnyObjectByType<Artifact>();
    }

    void Update()
    {
        if (continueButton == null) { return; }

        if (Input.GetKeyDown(KeyCode.E) && artifact.LevelCleared)
        {
            RaycastHit2D ray = Physics2D.BoxCast(transform.position, new Vector2(2, 2), 0, Vector2.up, detectionDistance, exitLayer);

            if (ray.collider != null)
            {
                LevelEnded = true;
                continueButton.gameObject.SetActive(true);
                powerManager.ShowKillText();
            }
        }
    }
}
