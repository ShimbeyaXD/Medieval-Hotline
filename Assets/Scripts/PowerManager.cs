using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PowerManager : MonoBehaviour
{
    [SerializeField] Slider holyometer;
    [SerializeField] Animator animator;
    [SerializeField] TextMeshProUGUI killText;
    [SerializeField] TextMeshProUGUI chargeText;
    [SerializeField] private float currentHolyness;
    [SerializeField] private float decreaseHolyness = 2.5f;
    [SerializeField] private Animator lShiftAnimator;

    //[SerializeField] GameObject glockImage;

    GameObject player;
    NewWeaponManager weaponManager;
    PlayerMovement playerMovement;
    Attack playerAttack;

    bool canDash = false;

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
        StartCoroutine(HolynessDecrease());
        chargeText.gameObject.SetActive(false);
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
                weaponManager = playerObject.GetComponent<NewWeaponManager>();
                playerMovement = playerObject.GetComponent<PlayerMovement>();
                playerAttack = playerObject.GetComponent<Attack>();
                break;
            }

            yield return new WaitForSeconds(1f);
        }


    }

    private void Update()
    {
        //currentHolyness = 100f; // DEBUG PURPOSE - TA BORT SEN 

        killText.text = "Kills: " + KillCount;

        HereMyBOIIIii();

        if (currentHolyness >= maxHolyness) 
        { 
            currentHolyness = maxHolyness; 
            canDash = true;
            lShiftAnimator.SetBool("CanCharge", true);
            chargeText.gameObject.SetActive(true);

        }
        /*
        if (currentHolyness < maxHolyness)
        {
            holyometer.transform.parent.gameObject.SetActive(true);
        }
        */


        if (Input.GetButtonDown("Fire3") && !playerAttack.PlayerIsAttacking && playerMovement.IsWalking && canDash && !playerMovement.Dead) // Charge
        {
            playerAttack.EnableCharge();
            canDash = false;
            animator.SetTrigger("Add");
            lShiftAnimator.SetBool("CanCharge", false);
            chargeText.gameObject.SetActive(false);
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
    

    IEnumerator HolynessDecrease() 
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
        //killText.gameObject.SetActive(true);
    }
}
