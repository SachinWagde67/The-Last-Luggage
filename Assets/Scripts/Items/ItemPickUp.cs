using System.Linq;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Items item;
    private Inventory inventory;

    private void Start()
    {
        inventory = Inventory.Instance;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (item.itemName == "Bag" && other.gameObject.GetComponent<PlayerController>() != null)
        {
            inventory.addSlot();
            Destroy(gameObject);
        }
        else if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            bool isItemPicked = inventory.addItem(item);
            if (isItemPicked)
            {
                Destroy(gameObject);
            }
            else
            {
                inventory.removeTxt.gameObject.SetActive(true);
                inventory.showItemInfo(this.item.icon, this.item.description);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<PlayerController>() != null)
        {
            inventory.itemInfo.SetActive(false);
            inventory.slotInfo.SetActive(false);
            inventory.removeTxt.gameObject.SetActive(false);
        }
    }
}
