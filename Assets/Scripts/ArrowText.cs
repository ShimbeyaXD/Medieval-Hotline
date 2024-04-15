using System.Collections;
using TMPro;
using UnityEngine;

public class ArrowText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI magazineText;

    NewWeaponManager newWeaponManager;
    Attack attack;

    void Start()
    {
        StartCoroutine(LookForAttackAndManager());
    }

    IEnumerator LookForAttackAndManager() 
    {
        attack = null;
        newWeaponManager = null;
        GameObject player = null;

        while (player == null || !player.activeSelf) 
        {
            player = GameObject.Find("Player");

            if (player != null)
            {
                attack = player.GetComponent<Attack>();
                newWeaponManager = player.GetComponent<NewWeaponManager>();
                break;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    void Update()
    {
        if(newWeaponManager == null)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            return; 
        }

        if (newWeaponManager.HasCrossbow || newWeaponManager.HasGlock)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            magazineText.text = attack.CurrentArrows.ToString();
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
