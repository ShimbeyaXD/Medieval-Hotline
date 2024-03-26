using System.Collections.Generic;
using UnityEngine;

public class BloodManager : MonoBehaviour
{
    [SerializeField] List<Sprite> boodSprites = new List<Sprite>();
    [SerializeField] Sprite corpseSprite;

    [SerializeField] float objectSize = 0.8f;
    [SerializeField] int numInSortingLayer = -25;

    public void SpawnBlood(Transform pos)
    {
        SpawnCorpse(pos);

        int i = Random.Range(0, boodSprites.Count);

        GameObject blood = new GameObject("Blood");
        blood.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
        blood.transform.position = pos.position;
        blood.transform.localScale = new Vector2(objectSize, objectSize);
        SpriteRenderer sp = blood.AddComponent<SpriteRenderer>();
        sp.sprite = boodSprites[i];
        sp.sortingOrder = numInSortingLayer;
    }

    void SpawnCorpse(Transform pos)
    {
        GameObject corpse = new GameObject("Corpse");
        corpse.transform.position = pos.position;
        corpse.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
        corpse.transform.localScale = new Vector2(objectSize, objectSize);
        SpriteRenderer sp = corpse.AddComponent<SpriteRenderer>();
        sp.sprite = corpseSprite;
        int newCorpsSortingLayer = numInSortingLayer;
        newCorpsSortingLayer++;
        sp.sortingOrder = newCorpsSortingLayer;
    }
}
