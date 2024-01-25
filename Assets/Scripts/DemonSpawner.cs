using UnityEngine;

public class DemonSpawner : MonoBehaviour
{
    Animator demonAnimator;

    void Start()
    {
        demonAnimator = GetComponent<Animator>(); 
    }

    public void SpawnDemons()
    {
        demonAnimator.SetBool("isCracking", true);

        // Spawn bao's demons, maybe make a collecrtive script to reference one instead of 1 million 
    }
}
