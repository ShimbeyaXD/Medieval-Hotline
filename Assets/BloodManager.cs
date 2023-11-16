using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodManager : MonoBehaviour
{
    [SerializeField] List<Sprite> boodSprites = new List<Sprite>();
    [SerializeField] LayerMask targetLayer;

    private void Start()
    {
        SpawnBlood(gameObject.transform);
    }
    public void SpawnBlood(Transform pos) 
    { 
       int i = Random.Range(0, boodSprites.Count);
        
       GameObject blood = new GameObject("Blood") ;
       SpriteRenderer sp = blood.AddComponent<SpriteRenderer>();

        if (sp != null ) 
        {
            sp.sprite = boodSprites[i];
            Instantiate(blood, pos.position, Quaternion.identity);
            blood.layer = targetLayer;
        }
        else 
        {
            Debug.Log("No spriteRenderer Found");
        }

    }
}
