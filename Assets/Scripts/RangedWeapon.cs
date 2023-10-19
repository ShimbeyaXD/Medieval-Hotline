using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    [SerializeField] int currentArrows = 5;

    public int NumberOfArrows()
    {
        currentArrows--;
        return currentArrows;
    }
}
