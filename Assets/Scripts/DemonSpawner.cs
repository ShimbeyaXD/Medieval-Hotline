using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonSpawner : MonoBehaviour
{
    [SerializeField] GameObject demon;
    [SerializeField] List<GameObject> pentagramList;
    [SerializeField] float spawnIntervals = 100f;

    Animator demonAnimator;
    Artifact artifact;
    Keeper keeper;

    bool once1 = true;
    bool once2 = true;

    void Start()
    {
        StartCoroutine(LookForArtifact());

        demonAnimator = GetComponent<Animator>();
        demonAnimator = GetComponent<Animator>(); 

        keeper = GameObject.FindGameObjectWithTag("Keeper").GetComponent<Keeper>();


    }

    IEnumerator LookForArtifact() 
    {
        artifact = null;

        while (artifact == null || !artifact.isActiveAndEnabled) 
        { 
            artifact = FindObjectOfType<Artifact>();
            yield return new WaitForSeconds(1f);
        }
    }

    private void Update()
    {
        if (artifact == null) { return; }
        if (keeper.IsLevelCleared && once1)
        {
            DemonSpawnInitiate();
        }
    }

    public void DemonSpawnInitiate()
    {
        once1 = false;
        keeper.DemonPhase = true;

        StartCoroutine(PlayAnimation());

        // Spawn bao's demons, maybe make a collecrtive script to reference one instead of 1 million 
    }

    IEnumerator PlayAnimation()
    {
        for (int i = 0; i < pentagramList.Count; i++)
        {
            demonAnimator = pentagramList[i].transform.GetComponent<Animator>();
            demonAnimator.SetBool("isCracking", true);
        }

        yield break;
    }

    public void SpawnDemons()
    {
        if (once2)
        {
            once2 = false;
            StartCoroutine(Spawning());
        }
    }

    IEnumerator Spawning()
    {
        for (int i = 0; i < pentagramList.Count; i++)
        {
            GameObject demonObject = Instantiate(demon, pentagramList[i].transform.position, Quaternion.identity);
        }

        yield return new WaitForSeconds(spawnIntervals);
        once1 = true;
        once2 = true;
    }
}
