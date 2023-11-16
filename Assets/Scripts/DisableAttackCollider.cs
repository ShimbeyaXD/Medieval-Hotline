using UnityEngine;

public class DisableAttackCollider : MonoBehaviour
{
    [SerializeField] BoxCollider2D attackCollider;
    [SerializeField] GameObject weaponObject;

    public void DisableCollider()
    {
        attackCollider.enabled = false;
        weaponObject.SetActive(true);
    }
}
