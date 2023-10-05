using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    BoxCollider2D boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { EnableMelee(); }
    }

    public void EnableMelee()
    {
        boxCollider.enabled = true;
    }

    public void DisableMelee()
    {
        boxCollider.enabled = false;
    }
}
