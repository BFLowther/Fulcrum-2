using UnityEngine;

public class Indicator : MonoBehaviour
{
    public Color indicatorColor;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Gun.SharedInstance.ClosestEnemy(gameObject.transform.parent.name))
        {
            spriteRenderer.color = indicatorColor;
        }
        else
            spriteRenderer.color = Color.white;
    }
}
