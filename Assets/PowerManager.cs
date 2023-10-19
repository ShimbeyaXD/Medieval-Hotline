using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.Experimental.GraphView;

public class PowerManager : MonoBehaviour
{
    private float maxHolyness = 100f;
   [SerializeField] private float currentHolyness;

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
        if(currentHolyness >= maxHolyness) { currentHolyness = maxHolyness; }
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
            if (currentHolyness > 0)
            {
                currentHolyness -= (Time.deltaTime * 2.69f);
                holyometer.value = currentHolyness;
            }
            yield return new WaitForEndOfFrame();
        }

        while(!alive)
        {
            yield return null;
        }


        
    }

    

    
}
