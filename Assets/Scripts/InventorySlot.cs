using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    Items items;
    public bool isEmpty;

    public void AddItem(Items newItem)
    {
        items = newItem;
        icon.sprite = items.icon;
        icon.enabled = true;
        isEmpty = false;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        items = null;
        icon.sprite = null;
        icon.enabled = false;
        isEmpty = true;
        removeButton.interactable = false;
    }

    public void onRemoveButton()
    {
        Inventory.instance.removeItem(items);
    }

    public void onSlotBtn()
    {
        if (items != null)
        {
            Inventory.instance.showSlotInfo(items.icon);
        }
    }
}
