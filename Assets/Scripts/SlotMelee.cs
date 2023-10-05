using UnityEngine;

public class SlotMelee : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void AddWeapon(Sprite gunSprite)
    {
        spriteRenderer.sprite = gunSprite;
    }

    public void RemoveWeapon()
    {
        spriteRenderer.sprite = null;
    }
}
