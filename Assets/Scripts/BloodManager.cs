using System.Collections.Generic;
using UnityEngine;

public class BloodManager : MonoBehaviour
{
    [SerializeField] List<Sprite> boodSprites = new List<Sprite>();
    [SerializeField] Sprite corpseSprite;

    [SerializeField] int numInSortingLayer = -25;

    private void Start()
    {
        //SpawnBlood(gameObject.transform);
    }

    public void SpawnBlood(Transform pos)
    {
        SpawnCorpse(pos);

        int i = Random.Range(0, boodSprites.Count);

        GameObject blood = new GameObject("Blood");
        blood.transform.position = pos.position;
        SpriteRenderer sp = blood.AddComponent<SpriteRenderer>();
        sp.sprite = boodSprites[i];
        sp.sortingOrder = numInSortingLayer;
    }

    void SpawnCorpse(Transform pos)
    {
        GameObject corpse = new GameObject("Corpse");
        corpse.transform.position = pos.position;
        SpriteRenderer sp = corpse.AddComponent<SpriteRenderer>();
        sp.sprite = corpseSprite;
        sp.sortingOrder = numInSortingLayer--;
    }
}
