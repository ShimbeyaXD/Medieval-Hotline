using UnityEngine;

public class SceneStarter : MonoBehaviour
{
    Keeper keeper;

    void Start()
    {
        Debug.Log("Start new song");
        keeper = GameObject.FindGameObjectWithTag("Keeper").GetComponent<Keeper>();
        keeper.MusicStereo();
    }
}
