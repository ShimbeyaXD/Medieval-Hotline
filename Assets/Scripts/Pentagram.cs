using System.Collections;
using UnityEngine;

public class Pentagram : MonoBehaviour
{
    DemonSpawner demonSpawner;

    bool spawnReady = false;
    bool coroutineInProgress = false;

    void Start()
    {
        demonSpawner = FindObjectOfType<DemonSpawner>();
    }

    public void DemonTime()
    {
        demonSpawner.SpawnDemons();
    }
}
