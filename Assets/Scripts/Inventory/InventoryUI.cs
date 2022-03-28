using System.Linq;
using UnityEngine;

public class InventoryUI : SingletonGeneric<InventoryUI>
{
    Inventory inventory;
    public Transform itemsParent;
    public GameObject[] slots;
    private int activeSlots;

    void Start()
    {
        updateSlots();
        inventory = Inventory.Instance;
        inventory.onItemChangedCallback += UpdateUI;
    }

    public void updateSlots()
    {
        slots = ObjectPool.Instance.slotPool.ToArray<GameObject>();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
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
    
    public int getSlotsCount()
    {
        return activeSlots;
    }

    private void OnDisable()
    {
        inventory.onItemChangedCallback -= UpdateUI;
    }
}
