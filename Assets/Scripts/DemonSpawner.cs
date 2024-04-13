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

    bool once1 = true;
    bool once2 = true;

    void Start()
    {
        demonAnimator = GetComponent<Animator>(); 
        artifact = FindObjectOfType<Artifact>();
    }

    private void Update()
    {
        if (artifact.LevelCleared && once1)
        {
            once1 = true;
            DemonSpawnInitiate();
        }
    }

    public void DemonSpawnInitiate()
    {
        StartCoroutine(PlayAnimation());

        // Spawn bao's demons, maybe make a collecrtive script to reference one instead of 1 million 
    }

    IEnumerator PlayAnimation()
    {
        for (int i = 0; i < pentagramList.Count; i++)
        {
            Debug.Log("Start animation");
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
            Debug.Log("Spawning enemies");

        }

        yield return new WaitForSeconds(spawnIntervals);
        once1 = true;
        once2 = true;
    }
}
