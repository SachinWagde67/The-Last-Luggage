using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryUI : SingletonGeneric<InventoryUI>
{
    Inventory inventory;
    public Transform itemsParent;
    public List<GameObject> slots;

    void Start()
    {
        updateSlots();
        inventory = Inventory.Instance;
        inventory.onItemChangedCallback += UpdateUI;
    }

    public void updateSlots()
    {
        slots = ObjectPool.Instance.slotPool;
    }

    private void UpdateUI()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if(i < inventory.items.Count)
            {
                slots[i].GetComponent<InventorySlot>().AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].GetComponent<InventorySlot>().ClearSlot();
            }
        }
    }

    private void OnDisable()
    {
        inventory.onItemChangedCallback -= UpdateUI;
    }
}
