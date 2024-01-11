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
       int i = Random.Range(0, boodSprites.Count);
        
       GameObject blood = new GameObject("Blood") ;
       blood.transform.position = pos.position;
       SpriteRenderer sp = blood.AddComponent<SpriteRenderer>();
        sp.sprite = boodSprites[i];
        sp.sortingOrder = -50;
        /*
        if (sp != null ) 
        {
           
            if(blood.layer == LayerMask.NameToLayer("Blood")) 
            {
            sp.sprite = boodSprites[i];
                Instantiate(blood, pos.position, Quaternion.identity);
                Debug.Log("Blood instantiated");
            }
            else { Debug.Log("Wrong Layer"); }
            
        }
        else 
        {
            Debug.Log("No spriteRenderer Found");
        }
        */
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
        int newCorpsSortingLayer = numInSortingLayer;
        newCorpsSortingLayer++;
        sp.sortingOrder = newCorpsSortingLayer;
    }
}
