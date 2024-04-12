using UnityEngine;

public class Pentagram : MonoBehaviour
{
    DemonSpawner demonSpawner;

    void Start()
    {
        demonSpawner = FindObjectOfType<DemonSpawner>();
    }

    public void DemonTime()
    {
        demonSpawner.SpawnDemons();
    }
}
