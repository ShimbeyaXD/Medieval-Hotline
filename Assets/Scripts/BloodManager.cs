using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BloodManager : MonoBehaviour
{
    [SerializeField] List<Sprite> boodSprites = new List<Sprite>();
    [SerializeField] List<Sprite> corpseSprite = new List<Sprite>();
    [SerializeField] Sprite demonSprite;

    [SerializeField] CapsuleCollider2D corpseCollider;
    [SerializeField] BoxCollider2D bloodCollider;

    [SerializeField] float objectSize = 0.8f;
    [SerializeField] int numInSortingLayer = -25;

    int bloodNum;
    int corpseNum;

    GameObject enemy;
    Keeper keeper;

    private void Start()
    {
        keeper = GameObject.FindGameObjectWithTag("Keeper").GetComponent<Keeper>();

        if (keeper.SearchAndDestroy(gameObject)) Destroy(gameObject);
    }

    public void SpawnBlood(Transform pos, GameObject sender, EnemyYEs enemyYes)
    {
        enemy = sender;
        bloodNum++;
        SpawnCorpse(pos, sender, enemyYes);

        int i = Random.Range(0, boodSprites.Count);

        GameObject blood = new GameObject("Blood");
        blood.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
        blood.transform.position = pos.position;
        blood.transform.localScale = new Vector2(objectSize, objectSize);
        blood.name = new string(blood.name + bloodNum);
        SpriteRenderer sp = blood.AddComponent<SpriteRenderer>();
        sp.sprite = boodSprites[i];
        sp.sortingOrder = numInSortingLayer;

        BoxCollider2D bc = blood.AddComponent<BoxCollider2D>();
        bc.size = bloodCollider.size;

        Rigidbody2D rb = blood.AddComponent<Rigidbody2D>();
        rb.isKinematic = true;

        keeper.BloodInstance(GetComponent<BloodManager>(), blood);
    }

    void SpawnCorpse(Transform pos, GameObject sender, EnemyYEs enemyYes)
    {
        corpseNum++;
        GameObject corpse = new GameObject("Corpse");
        corpse.transform.position = pos.position;
        corpse.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
        corpse.transform.localScale = new Vector2(objectSize, objectSize);
        corpse.name = new string(corpse.name + corpseNum);
        SpriteRenderer sp = corpse.AddComponent<SpriteRenderer>();

        CapsuleCollider2D pc = corpse.AddComponent<CapsuleCollider2D>();
        pc.size = corpseCollider.size;
        pc.direction = corpseCollider.direction;

        Rigidbody2D rb = corpse.AddComponent<Rigidbody2D>();
        rb.isKinematic = true;

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

        keeper.CorpseInstance(GetComponent<BloodManager>(), corpse);
        keeper.EnemyInstance(enemyYes, sender);
    }

    public void Replace()
    {
        Destroy(gameObject);
    }
}
