using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : SingletonGeneric<Inventory>
{
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public List<Items> items = new List<Items>();
    public int space;
    public GameObject slotInfo;
    public Image slotInfoIcon;
    public TextMeshProUGUI slotInfoDescription;
    public GameObject itemInfo;
    public Image itemInfoIcon;
    public TextMeshProUGUI itemInfoDescription;
    public TextMeshProUGUI removeTxt;

    public bool addItem(Items item)
    {
        if(items.Count >= space)
        {
            return false;
        }

        items.Add(item);
        onItemChangedCallback?.Invoke();
        return true;
    }    

    public void removeItem(Items item)
    {
        items.Remove(item);
        slotInfo.SetActive(false);
        onItemChangedCallback?.Invoke();
    }

    public void showSlotInfo(Sprite icon, string description)
    {
        slotInfoIcon.sprite = icon;
        slotInfoDescription.text = description;
        slotInfo.SetActive(true);
    }

    public void showItemInfo(Sprite icon, string description)
    {
        itemInfoIcon.sprite = icon;
        itemInfoDescription.text = description;
        itemInfo.SetActive(true);
    }

    public void addSlot()
    {
        GameObject slot = ObjectPool.Instance.GetFromPool();
        slot.SetActive(true);
        slot.GetComponent<InventorySlot>().isEmpty = true;
        space++;
    }

    public void removeSlot(bool isEmptySlot)
    {
        if (!isEmptySlot)
        {
            removeItem(items[items.Count - 1]);
        }
        ObjectPool.Instance.AddBackToPool();
        space--;
    }
}
