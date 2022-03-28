using UnityEngine;

public class InventoryUI : SingletonGeneric<InventoryUI>
{
    Inventory inventory;
    public Transform itemsParent;
    public InventorySlot[] slots;

    void Start()
    {
        updateSlots();
        inventory = Inventory.Instance;
        inventory.onItemChangedCallback += UpdateUI;
    }

    private void updateSlots()
    {
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    private void UpdateUI()
    {
        updateSlots();
        for (int i = 0; i < slots.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
    
    public int getSlotsCount()
    {
        updateSlots();
        return slots.Length;
    }

    private void OnDisable()
    {
        inventory.onItemChangedCallback -= UpdateUI;
    }
}
