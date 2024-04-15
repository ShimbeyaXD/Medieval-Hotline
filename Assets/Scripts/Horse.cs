using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Horse : MonoBehaviour
{
    [SerializeField] List<bool> sceneAnimationNumber = new List<bool>();

    [SerializeField] Transform myTransfrom;
    [SerializeField] Transform endPosition;
    [SerializeField] GameObject realHorse;
    [SerializeField] Canvas normalCanvas;
    [SerializeField] Canvas extraCanvas;

    [Header("PlayerOffset")]
    [SerializeField] float xOffset;
    [SerializeField] float yOffset;

    [SerializeField] float speed;

    private Vector3 velocity = Vector3.zero;
    private bool reachedEnd = false;

    Keeper keeper;
    Animator openingAnimator;
    GameObject playerLights;

    private void Awake()
    {
        endPosition.gameObject.SetActive(true);

        openingAnimator = endPosition.gameObject.GetComponent<Animator>();
        openingAnimator.enabled = false;
        playerLights = transform.GetChild(2).gameObject;

        keeper = GameObject.FindGameObjectWithTag("Keeper").GetComponent<Keeper>();

        keeper.ManagerInstance(realHorse);

        //if (keeper.SearchAndDestroy(realHorse)) Destroy(realHorse.gameObject);
        keeper.LevelEnded = false;

        if (keeper.PlayOpeningAnimation)
        {

            normalCanvas.enabled = false;
            playerLights.SetActive(true);
        }
        else
        {
            playerLights.SetActive(false);
        }

        endPosition.position += new Vector3(xOffset, yOffset, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (!reachedEnd && keeper.PlayOpeningAnimation)
        {
            openingAnimator.enabled = true;

            endPosition.gameObject.SetActive(false);

            transform.position = Vector3.SmoothDamp(myTransfrom.position, endPosition.position, ref velocity, speed);

            if (Vector3.Distance(myTransfrom.position, endPosition.position) < 0.2f)
            {
                reachedEnd = true;
                keeper.PlayOpeningAnimation = false;

                endPosition.gameObject.SetActive(true);
                realHorse.SetActive(true);

                openingAnimator.enabled = true;

                StartAnimation();
                gameObject.SetActive(false);
            }
        }        
    }

    void StartAnimation()
    {
        if (keeper.LevelEnded) return;

        normalCanvas.enabled = true;

        if (sceneAnimationNumber[0])
        {
            openingAnimator.SetBool("isScene1", true);

        }
        if (sceneAnimationNumber[1])
        {
            openingAnimator.SetBool("isScene2", true);

        }

        if (sceneAnimationNumber[2])
        {
            openingAnimator.SetBool("isScene3", true);

        }

        else
        {
            Debug.LogWarning("No scene animation");
            return;
        }
    }
}
