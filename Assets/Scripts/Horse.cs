using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Horse : MonoBehaviour
{
    [SerializeField] List<bool> sceneAnimationNumber = new List<bool>();

    [SerializeField] Transform myTransfrom;
    [SerializeField] Transform endPosition;

    [SerializeField] float speed;

    private Vector3 velocity = Vector3.zero;
    private bool reachedEnd = false;

    Keeper keeper;
    Animator openingAnimator;
    Canvas canvas;
    GameObject playerLights;

    private void Awake()
    {
        openingAnimator = endPosition.gameObject.GetComponent<Animator>();
        openingAnimator.enabled = false;
        playerLights = transform.GetChild(2).gameObject;

        keeper = GameObject.FindGameObjectWithTag("Keeper").GetComponent<Keeper>();

        if (keeper.PlayOpeningAnimation)
        {
            canvas = FindObjectOfType<Canvas>();
            canvas.enabled = false;
            playerLights.SetActive(true);
        }
        else
        {
            playerLights.SetActive(false);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (!reachedEnd && keeper.PlayOpeningAnimation)
        {
            openingAnimator.enabled = true;

            endPosition.gameObject.SetActive(false);

            transform.position = Vector3.SmoothDamp(myTransfrom.position, endPosition.position + new Vector3(0.5f, 0, 0), ref velocity, speed);

            if (Vector3.Distance(myTransfrom.position, endPosition.position + new Vector3(0.24f, 0, 0)) < 0.2f)
            {
                reachedEnd = true;
                keeper.PlayOpeningAnimation = false;

                endPosition.gameObject.SetActive(true);
                openingAnimator.enabled = true;

                StartAnimation();
                gameObject.SetActive(false);
            }
        }        
    }

    void StartAnimation()
    {
        canvas.enabled = true;

        if (sceneAnimationNumber[0])
        {
            openingAnimator.SetBool("isScene1", true);
        }
        if (sceneAnimationNumber[1])
        {
            openingAnimator.SetBool("isScene2", true);
        }
        else
        {
            Debug.LogWarning("No scene animation");
            return;
        }
    }
}
