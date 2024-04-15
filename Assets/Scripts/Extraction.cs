using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Extraction : MonoBehaviour
{
    [Header("Extracting Stage")]
    [SerializeField] LayerMask exitLayer;
    [SerializeField] float detectionDistance = 2;
    [SerializeField] GameObject continueButton;

    [Header("Reloading Stage")]
    [SerializeField] float reloadSceneDelay = 2;
    [SerializeField] GameObject restartText;
    [SerializeField] Animator animatorUI;

    Artifact artifact;
    RoundTimer roundTimer;
    PowerManager powerManager;
    Keeper keeper;
    PlayerMovement playerMovement;
    TextMeshProUGUI text;
    SceneLoader sceneLoader;

    bool inputIntermission = false;

    public bool LevelEnded { get; private set; }

    void Start()
    {
        text = restartText.GetComponent<TextMeshProUGUI>();

        text.enabled = false;
        restartText.SetActive(false);
        continueButton.gameObject.SetActive(false);

        roundTimer = FindObjectOfType<RoundTimer>();
        powerManager = FindObjectOfType<PowerManager>();
        artifact = GetComponent<Artifact>();
        playerMovement = GetComponent<PlayerMovement>();

        keeper = GameObject.FindGameObjectWithTag("Keeper").GetComponent<Keeper>();
    }

    void Update()
    {
        Extracting();

        if (Input.GetButtonDown("Jump") && inputIntermission && playerMovement.Dead) { StartCoroutine(ReloadScene(false)); StopCoroutine(InputIntermission()); }
        if (Input.GetButtonDown("Jump") && inputIntermission && !playerMovement.Dead) { StartCoroutine(ReloadScene(true)); StopCoroutine(InputIntermission()); }

    }


    void Extracting()
    {
        if (Input.GetButtonDown("Submit") && keeper.IsLevelCleared)
        {
            RaycastHit2D ray = Physics2D.BoxCast(transform.position, new Vector2(2, 2), 0, Vector2.up, detectionDistance, exitLayer);

            if (ray.collider != null)
            {
                keeper.LevelEnded = true;
                continueButton.gameObject.SetActive(true);
                powerManager.ShowKillText();
                keeper.StageEnd();

                StartCoroutine(InputIntermission());
                
            }
        }
    }
    
    public void DeathScreen()
    {
        if (playerMovement.Dead)
        {
            restartText.SetActive(true);

            StartCoroutine(InputIntermission());
            StartCoroutine(TextAppearCooldown());
        }
    }

    IEnumerator TextAppearCooldown()
    {
        yield return new WaitForSeconds(1.2f);

        restartText.GetComponent<TextMeshProUGUI>().enabled = true;
    }

    IEnumerator InputIntermission()
    {
        while (true)
        {
            inputIntermission = true;

            yield return new WaitForEndOfFrame();

        }
    }

    IEnumerator ReloadScene(bool sceneChange)
    {
        animatorUI.SetBool("isFading", true);

        yield return new WaitForSeconds(reloadSceneDelay);

        keeper.WipeLists();

        Scene scene = SceneManager.GetActiveScene();

        int sceneNum = SceneManager.GetActiveScene().buildIndex;

        if (sceneChange) { SceneManager.LoadScene(sceneNum + 1); }
        if (!sceneChange) { SceneManager.LoadScene(scene.name); }
    }

}
