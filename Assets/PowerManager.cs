using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;




public class PowerManager : MonoBehaviour
{
    private float maxHolyness = 100f;
    private float currentHolyness;

    TextMeshProUGUI holyometer;

    

   void Start() 
   {
        currentHolyness = 0f;
        holyometer.text = "Holyness:" + currentHolyness;

   }    

    public void addHoliness(float holynessToAdd) 
    { 
      currentHolyness += holynessToAdd;
    }

    
}
