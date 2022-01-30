using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;

    private void Awake()
    {
        if(instance != null)
        {
            return;
        }
        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public List<Items> items = new List<Items>();
    public int space;
    public GameObject slotInfo;
    public Image slotInfoIcon;
    public GameObject itemInfo;
    public Image itemInfoIcon;
    public TextMeshProUGUI removeTxt;
    public Transform itemsParent; 
    public GameObject inventorySlotPrefab;

    private void Start()
    {
        slotInfo.SetActive(false);
        itemInfo.SetActive(false);
        removeTxt.gameObject.SetActive(false);
    }

    public bool addItem(Items item)
    {
        if(items.Count >= space)
        {
            return false;
        }

        items.Add(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
        return true;
    }    

    public void removeItem(Items item)
    {
        items.Remove(item);
        slotInfo.SetActive(false);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }

    public void showSlotInfo(Sprite icon)
    {
        slotInfoIcon.sprite = icon;
        slotInfo.SetActive(true);
    }

    public void showItemInfo(Sprite icon)
    {
        itemInfoIcon.sprite = icon;
        itemInfo.SetActive(true);
    }

    public void addSlot()
    {
        Instantiate(inventorySlotPrefab, itemsParent);
        space++;
    }

    public void removeSlot(bool isEmptySlot)
    {
        Transform lastSlot = itemsParent.GetChild(itemsParent.childCount - 1);
        if (!isEmptySlot)
        {
            removeItem(items[items.Count - 1]);
        }
        Destroy(lastSlot.gameObject);
        space--;
    }
}
