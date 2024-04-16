using UnityEngine;

public class Horse : MonoBehaviour
{
    private void OnEnable()
    {
        Keeper keeper = GameObject.FindGameObjectWithTag("Keeper").GetComponent<Keeper>();

        if (keeper.SearchAndDestroy(this.gameObject)) { Destroy(this.gameObject); }

        keeper.HorseInstance(this.gameObject);
    }
}
