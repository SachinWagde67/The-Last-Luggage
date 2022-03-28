using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : SingletonGeneric<ObjectPool>
{
    [SerializeField] private int poolSize;
    [SerializeField] private int initialSlots;
    private int activeSlots;

    public Transform itemsParent;
    public GameObject inventorySlotPrefab;
    public List<GameObject> slotPool;

    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        slotPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            if(i < initialSlots)
            {
                GameObject slot = createSlot();
                slot.SetActive(true);
            }
            else
            {
                GameObject slot = createSlot();
                slot.SetActive(false);
            }
        }
    }

    public int getActiveSlotSize()
    {
        activeSlots = 0;
        for (int i = 0; i < slotPool.Count; i++)
        {
            if (slotPool[i].activeSelf)
            {
                activeSlots++;
            }
        }
        return activeSlots;
    }

    private GameObject createSlot()
    {
        GameObject createdslot = Instantiate(inventorySlotPrefab, itemsParent);
        slotPool.Add(createdslot);
        return createdslot;
    }

    public void AddBackToPool()
    {
        Transform lastSlot = InventoryUI.Instance.itemsParent.GetChild(ObjectPool.Instance.getActiveSlotSize() - 1);
        lastSlot.gameObject.SetActive(false);
    }

    public GameObject GetFromPool()
    {
        if (slotPool.Count == getActiveSlotSize())
        {
            GameObject slot = createSlot();
            slot.SetActive(false);
            InventoryUI.Instance.updateSlots();
        }
        GameObject lastSlot = slotPool[getActiveSlotSize()];
        return lastSlot;
    }

}
