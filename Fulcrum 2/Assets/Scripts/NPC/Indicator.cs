using UnityEngine;

public class Indicator : MonoBehaviour
{
    Gun playersGunScript;
    public GameObject playersGun;
    public Transform player;
    public Color indicatorColor;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        playersGunScript = playersGun.GetComponent<Gun>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Debug.Log(gameObject.transform.parent.name);
    }

    private void Update()
    {
        if (playersGunScript.ClosestEnemy(player.position, gameObject.transform.parent.name))
        {
            spriteRenderer.color = indicatorColor;
        }
        else
            spriteRenderer.color = Color.white;
    }
}
