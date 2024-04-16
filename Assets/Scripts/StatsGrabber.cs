using TMPro;
using UnityEngine;

public class StatsGrabber : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI deathText1;
    [SerializeField] TextMeshProUGUI deathText2;
    [SerializeField] TextMeshProUGUI deathText3;

    Keeper keeper;

    private void Awake()
    {
        keeper = GameObject.FindGameObjectWithTag("Keeper").GetComponent<Keeper>();
    }

    private void Update()
    {
        if (keeper != null)
        {
            GrabStats();
        }
    }

    public void GrabStats()
    {
        Debug.Log(keeper.deathCount3.ToString()+ " is the deathcount3");
        deathText1.text = keeper.deathCount1.ToString();
        deathText2.text = keeper.deathCount2.ToString();
        deathText3.text = keeper.deathCount3.ToString();
    }
}
