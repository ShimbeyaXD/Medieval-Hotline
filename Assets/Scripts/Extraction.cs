using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Extraction : MonoBehaviour
{
    [Header("Extraction Stage")]
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float detectionDistance = 0.8f;
    [SerializeField] GameObject nextLevelButtonObject;
    [SerializeField] Animator fadeAnimator;

    [Header("Reloading Stage")]
    [SerializeField] float reloadSceneDelay = 1.5f;
    [SerializeField] GameObject restartText;
    [SerializeField] Animator restartTextAnimator;


    PowerManager powerManager;
    Keeper keeper;
    TextMeshProUGUI text;

    bool deathScreen = false;

    public bool LevelEnded { get; private set; }

    void OnEnable()
    {
        keeper = GameObject.FindGameObjectWithTag("Keeper").GetComponent<Keeper>();

        //restartText = GameObject.Find("RestartText");
        //animatorUI = restartText.GetComponent<Animator>();

        deathScreen = false;
        text = restartText.GetComponent<TextMeshProUGUI>();
        text.enabled = false;
        restartText.SetActive(false);
        nextLevelButtonObject.gameObject.SetActive(false);

        powerManager = FindAnyObjectByType<PowerManager>();
    }

    void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, detectionDistance, playerLayer) && keeper.IsLevelCleared) // Player Extracted
        {
            if (nextLevelButtonObject == null) { nextLevelButtonObject = GameObject.Find("NextLevelButton"); }            

            nextLevelButtonObject.SetActive(true);

            if (Input.GetButtonDown("Jump")) // Space on keyboard
            {
                StartCoroutine(ReloadScene(true));
                keeper.LevelEnded = true;
                powerManager.ShowKillText();
                keeper.StageEnd();
                keeper.PlayOpeningAnimation = true;
                fadeAnimator.SetTrigger("BlackScreen");
            }
        }
        if (!Physics2D.OverlapCircle(transform.position, detectionDistance, playerLayer))
        {
            nextLevelButtonObject.SetActive(false);
        }

        if (Input.GetButtonDown("Fire1") && deathScreen) { StartCoroutine(ReloadScene(false)); }

        //if (Input.GetButtonDown("Fire1") && deathScreen && !playerMovement.Dead) { StartCoroutine(ReloadScene(true)); }

    }


    void Extracting()
    {
        /*
        if (Input.GetButtonDown("Submit") && keeper.IsLevelCleared)
        {
            RaycastHit2D ray = Physics2D.BoxCast(transform.position, new Vector2(2, 2), 0, Vector2.up, detectionDistance, exitLayer);

            if (ray.collider != null)
            {

                StartCoroutine(InputIntermission());
                
            }
        }
        */
    }
    
    public void DeathScreen()
    {
        deathScreen = true;
        restartText.SetActive(true);

        StartCoroutine(TextAppearCooldown());
        //StartCoroutine(InputIntermission());
    }

    IEnumerator TextAppearCooldown()
    {
        yield return new WaitForSeconds(1.5f);

        text.enabled = true;
    }

    IEnumerator ReloadScene(bool sceneChange)
    {
        restartTextAnimator.SetBool("isFading", true);

        yield return new WaitForSeconds(reloadSceneDelay);

        Scene scene = SceneManager.GetActiveScene();

        int sceneNum = SceneManager.GetActiveScene().buildIndex;

        keeper.WipeLists(false);

        if (sceneChange) 
        {
            keeper.IsLevelCleared = false;
            SceneManager.LoadScene(sceneNum + 1); 
        }

        if (!sceneChange) { SceneManager.LoadScene(scene.name); }
    }

}
