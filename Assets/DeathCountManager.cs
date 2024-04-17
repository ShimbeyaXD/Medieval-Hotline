using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathCountManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] deathTexts = new TextMeshProUGUI[3];
     [SerializeField] Keeper keeper;
    // Start is called before the first frame update
    /*
    void Start()
    {
        deathTexts[0].text = keeper.GetDeathStage1().ToString();
        deathTexts[1].text = keeper.GetDeathStage2().ToString();
        deathTexts[2].text = keeper.GetDeathStage3().ToString();

    }
    */

}
