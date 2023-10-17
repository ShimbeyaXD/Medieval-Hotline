using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PowerManager : MonoBehaviour
{
    private float maxHolyness = 100f;
    private float currentHolyness;

    [SerializeField] Slider holyometer;

    bool alive = true;


   void Start() 
   {
        currentHolyness = 0f;
        holyometer.value = currentHolyness;
        StartCoroutine(HolynessFade());

   }

    private void Update()
    {
        HereMyBOIIIii();
    }

    public void addHoliness(float holynessToAdd) 
    { 
        currentHolyness += holynessToAdd;
        holyometer.value = currentHolyness;
    }


    void HereMyBOIIIii() 
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            addHoliness(20f);
        }
    }

    IEnumerator HolynessFade() 
    {
        while (alive) 
        {
            while (currentHolyness > 0)
            {
                currentHolyness -= Time.deltaTime;
            }
        }

        yield return null;
        
    }

    

    
}
