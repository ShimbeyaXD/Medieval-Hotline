using System.Collections.Generic;
using UnityEngine;

public class BloodManager : MonoBehaviour
{
    [SerializeField] List<Sprite> boodSprites = new List<Sprite>();
    [SerializeField] List<Sprite> corpseSprite = new List<Sprite>();
    [SerializeField] Sprite demonSprite;

    [SerializeField] float objectSize = 0.8f;
    [SerializeField] int numInSortingLayer = -25;

    GameObject enemy;

    public void SpawnBlood(Transform pos, GameObject sender)
    {
        enemy = sender;
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

        if (enemy.GetComponent<EnemyYEs>().ReturnDemonType())
        {
            sp.sprite = demonSprite;
        }
        else
        {
            int randomNum = Random.Range(0, 2);
            if (randomNum <= 0) { sp.sprite = corpseSprite[0]; }
            if (randomNum >= 1) { sp.sprite = corpseSprite[1]; }
        }

        int newCorpsSortingLayer = numInSortingLayer;
        newCorpsSortingLayer++;
        sp.sortingOrder = newCorpsSortingLayer;
    }
}
