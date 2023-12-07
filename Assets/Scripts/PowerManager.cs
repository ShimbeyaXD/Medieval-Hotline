using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PowerManager : MonoBehaviour
{
    private float maxHolyness = 100f;
   [SerializeField] private float currentHolyness;

    [SerializeField] Slider holyometer;
    [SerializeField] Animator animator;

    bool alive = true;


   void Start() 
   {
        currentHolyness = 0f;
        holyometer.value = currentHolyness;
        StartCoroutine(HolynessFade());

   }

    private void Update()
    {
        Debug.Log(currentHolyness);
        HereMyBOIIIii();
        if(currentHolyness >= maxHolyness) { currentHolyness = maxHolyness; }
    }

    public void AddHoliness(float holynessToAdd) 
    {
        currentHolyness += holynessToAdd;
        holyometer.value = currentHolyness;
        animator.SetTrigger("Add");
    }


    void HereMyBOIIIii() 
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddHoliness(20f);
        }
    }

    IEnumerator HolynessFade() 
    {
        while (alive)
        {
            if (currentHolyness > 0 && currentHolyness < maxHolyness)
            {
                currentHolyness -= (Time.deltaTime);
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
