using UnityEngine;

public class Cloud : MonoBehaviour
{
    private bool enable;
    private SpriteRenderer CloudSprite;

    private void Start()
    {
        CloudSprite = GetComponent<SpriteRenderer>();    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterController2D>() != null)
        {
            enable = true;
            CloudOff();
        }
    }

    private async void CloudOff()
    {
        await new WaitForSeconds(1.3f);

        while (enable)
        {
            CloudSprite.enabled = false;
            await new WaitForSeconds(.1f);
            CloudSprite.enabled = true;
            await new WaitForSeconds(.1f);
            await new WaitForSeconds(0.5f);
            enable = false;
        }

        gameObject.SetActive(false);
        await new WaitForSeconds(2f);
        CloudOn();

    }

    private void CloudOn()
    {
        gameObject.SetActive(true);
        CloudSprite.enabled = true;
    }
}
