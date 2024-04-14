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
            player = GameObject.Find("player");
            yield return new WaitForSeconds(1f);
        }

        attack = player.GetComponent<Attack>();
        newWeaponManager = player.GetComponent<NewWeaponManager>();
    }

    void Update()
    {
        if(newWeaponManager == null || attack == null) { return; }

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
