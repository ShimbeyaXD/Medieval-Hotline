using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PowerManager : MonoBehaviour
{
    [SerializeField] Slider holyometer;
    [SerializeField] Animator animator;
    [SerializeField] TextMeshProUGUI killText;
    [SerializeField] private float currentHolyness;

    public int KillCount { get; set; }

    private float maxHolyness = 100f;

    bool alive = true;

   void Start() 
   {
        KillCount = 0;
        currentHolyness = 0f;
        holyometer.value = currentHolyness;
        killText.gameObject.SetActive(false);
        StartCoroutine(HolynessFade());
   }

    private void Update()
    {
        killText.text = "Kills: " + KillCount;

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

    public void ShowKillText()
    {
        killText.gameObject.SetActive(true);
    }
}
