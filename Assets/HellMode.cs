using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HellMode : MonoBehaviour
{
    [SerializeField] GameObject redMist;
    [SerializeField] float alphaAmount = 0.5f;

    Extraction extraction;

    //Follow Target for 'camerashake'
    FollowTarget followTarget;
    // Start is called before the first frame update

    //DemonSpawning
    DemonSpawner demonSpawner;

    //Objective Text UI
    ObjectiveUI objectiveUI;

    void OnEnable()    
    {
        followTarget = FindObjectOfType<FollowTarget>();
        extraction = FindObjectOfType<Extraction>();
        demonSpawner = FindObjectOfType<DemonSpawner>();
        objectiveUI = FindObjectOfType<ObjectiveUI>();
        redMist.SetActive(true);

        CamreaShake();
        OppenCracks();

    }

    void CamreaShake()  
    {
        followTarget.StartShake(1.3f, 0.2f);
    }

    void OppenCracks() 
    {
        demonSpawner.DemonSpawnInitiate();
        Debug.Log("objectiveui is " + objectiveUI);
        objectiveUI.GetOutText();
    }

    public void FillOpacity()
    {
        StartCoroutine(RedTransition());
    }

    IEnumerator RedTransition()
    {
        while (true)
        {
            Color red = redMist.GetComponent<Image>().color;

            red += red * alphaAmount * Time.deltaTime;

            redMist.GetComponent<Image>().color = red;

            yield return new WaitForEndOfFrame();
        }

    }
}
