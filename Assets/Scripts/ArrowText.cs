using TMPro;
using UnityEngine;

public class ArrowText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI magazineText;

    NewWeaponManager newWeaponManager;
    Attack attack;

    void Start()
    {
        attack = FindObjectOfType<Attack>();
        newWeaponManager = FindObjectOfType<NewWeaponManager>();
    }

    void Update()
    {
        if (newWeaponManager.HasCrossbow)
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
