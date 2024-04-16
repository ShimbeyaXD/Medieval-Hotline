using UnityEngine;

public class SceneStarter : MonoBehaviour
{
    Keeper keeper;

    void Start()
    {
        keeper = GameObject.FindGameObjectWithTag("Keeper").GetComponent<Keeper>();
        keeper.MusicStereo();
    }
}
