using UnityEngine;

public class PushButton : MonoBehaviour
{
    [SerializeField] private GameObject buttonNotPressed;
    [SerializeField] private GameObject buttonPressed;
    [SerializeField] private GameObject door;
    private Animator doorAnimator;

    private void Start()
    {
        buttonNotPressed.SetActive(true);
        buttonPressed.SetActive(false);
        doorAnimator = door.GetComponent<Animator>();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            buttonNotPressed.SetActive(false);
            buttonPressed.SetActive(true);
            doorAnimator.SetBool("open", true);
            door.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            buttonNotPressed.SetActive(true);
            buttonPressed.SetActive(false);
            doorAnimator.SetBool("open", false);
            door.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
