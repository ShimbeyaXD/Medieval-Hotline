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
    [SerializeField] private float decreaseHolyness = 2.5f;
    [SerializeField] GameObject glockImage;

    GameObject player;
    NewWeaponManager weaponManager;
    PlayerMovement playerMovement;
    Attack playerAttack;

    bool canRecieveGlock = false;

    public int KillCount { get; set; }

    private float maxHolyness = 100f;

    bool alive = true;

   void Start() 
   {

        StartCoroutine(LookForPlayer());

        KillCount = 0;
        currentHolyness = 0f;
        holyometer.value = currentHolyness;
        killText.gameObject.SetActive(false);
        //glockImage.gameObject.SetActive(false);
        StartCoroutine(HolynessFade());
        currentHolyness = 100;
   }

    IEnumerator LookForPlayer()
    {
        while (player == null || !player.gameObject.activeSelf)
        {
            GameObject playerObject = GameObject.Find("Player");

            // Check if playerObject is not null before accessing its transform
            if (playerObject != null)
            {
                player = playerObject;
            }

            yield return new WaitForSeconds(1f);
        }

        weaponManager = FindObjectOfType<NewWeaponManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerAttack = FindObjectOfType<Attack>();
    }

    private void Update()
    {
        //currentHolyness = 100f; // DEBUG PURPOSE - TA BORT SEN 

        killText.text = "Kills: " + KillCount;

        HereMyBOIIIii();

        if (currentHolyness >= maxHolyness) 
        { 
            currentHolyness = maxHolyness; 
            canRecieveGlock = true; 
            glockImage.transform.GetChild(0).GetComponent<Animator>().SetBool("isActive", true);
            holyometer.transform.parent.gameObject.SetActive(false);
            glockImage.gameObject.SetActive(true);
        }

        if (currentHolyness < maxHolyness)
        {
            holyometer.transform.parent.gameObject.SetActive(true);
            glockImage.gameObject.SetActive(false);
        }

        if (Input.GetButtonDown("Fire3") && !playerAttack.PlayerIsAttacking && playerMovement.IsWalking && canRecieveGlock) // Charge
        {
            playerAttack.EnableCharge();
            //RecieveGlock();
            canRecieveGlock = false;
            glockImage.transform.GetChild(0).GetComponent<Animator>().SetBool("isActive", false);
            animator.SetTrigger("Add");
            currentHolyness = 0;
            holyometer.value = currentHolyness;
        }
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
    
    void RecieveGlock() // OBSELETE
    {
        weaponManager.Glock();
    }

    IEnumerator HolynessFade() 
    {
        while (alive)
        {
            if (currentHolyness > 0 && currentHolyness < maxHolyness)
            {
                currentHolyness -= (Time.deltaTime * decreaseHolyness);
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
