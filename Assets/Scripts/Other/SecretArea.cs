using UnityEngine;

public class SecretArea : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Tiles;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            foreach (GameObject tile in Tiles)
            {
                tile.SetActive(false);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            foreach (GameObject tile in Tiles)
            {
                tile.SetActive(true);
            }
        }
    }
}
