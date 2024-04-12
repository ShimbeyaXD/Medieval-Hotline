using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellMode : MonoBehaviour
{
    [SerializeField] GameObject redMist;

    // Start is called before the first frame update
    void OnEnable()    
    {
        redMist.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
