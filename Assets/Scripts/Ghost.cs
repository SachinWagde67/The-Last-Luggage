using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool canPatrol;
    [SerializeField] private CharacterController2D controller;
    [SerializeField] private GameObject[] waypoint;
    private bool movingRight = false;
    private Inventory inventory;
    private int currentIndex = 0;

    private void Start()
    {
        inventory = Inventory.instance;
    }

    private void Update()
    {
        if (!canPatrol)
        {
            if (Vector2.Distance(waypoint[currentIndex].transform.position, transform.position) < 0.1f)
            {
                currentIndex++;
                if (currentIndex >= waypoint.Length)
                {
                    currentIndex = 0;
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, waypoint[currentIndex].transform.position, speed * Time.deltaTime);
        }
        if (canPatrol)
        {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("waypoint") && canPatrol)
        {
            movingRight = !movingRight;
        }
        if (other.gameObject.GetComponent<Teleporter>() != null)
        {
            controller.destroyTeleporter();
        }
        if (other.gameObject.GetComponent<CharacterController2D>() != null)
        {
            int slotCount = other.gameObject.GetComponent<CharacterController2D>().inventoryUI.getSlotsCount();
            if (slotCount > 1)
            {
                inventory.removeSlot(other.gameObject.GetComponent<CharacterController2D>().inventoryUI.slots[slotCount - 1].isEmpty);
            }
            if (slotCount == 1 && inventory.items.Count > 0)
            {
                inventory.removeItem(inventory.items[inventory.items.Count - 1]);
            }
        }
    }
}
