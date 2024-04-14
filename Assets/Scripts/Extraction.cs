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

        if (Input.GetButtonDown("Jump") && inputIntermission) { Debug.Log("hey jump pressed"); StartCoroutine(ReloadScene()); StopCoroutine(InputIntermission()); }

    }


    void Extracting()
    {
        if (Input.GetButtonDown("Submit") && keeper.IsLevelCleared)
        {
            RaycastHit2D ray = Physics2D.BoxCast(transform.position, new Vector2(2, 2), 0, Vector2.up, detectionDistance, exitLayer);

            if (ray.collider != null)
            {
                LevelEnded = true;
                continueButton.gameObject.SetActive(true);
                powerManager.ShowKillText();
                keeper.StageEnd();
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
        yield return new WaitForSeconds(0.5f);

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

    IEnumerator ReloadScene()
    {
        animatorUI.SetBool("isFading", true);

        yield return new WaitForSeconds(reloadSceneDelay);

        keeper.WipeLists();

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

}
