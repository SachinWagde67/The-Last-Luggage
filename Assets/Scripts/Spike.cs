﻿using UnityEngine;

public class Spike : MonoBehaviour
{
    private Inventory inventory;
    private bool canRemoveSlot;
    private float removeSlotTimer;
    private float timer = 2f;

    private void Start()
    {
        inventory = Inventory.instance;
        canRemoveSlot = false;
        removeSlotTimer = timer;
    }

    private void Update()
    {
        if (canRemoveSlot)
        {
            removeSlotTimer -= Time.deltaTime;
        }
        if (removeSlotTimer <= 0f)
        {
            canRemoveSlot = false;
            removeSlotTimer = timer;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        CharacterController2D controller = other.gameObject.GetComponent<CharacterController2D>();
        if (controller != null && !canRemoveSlot)
        {
            canRemoveSlot = true;
            int slotCount = controller.inventoryUI.getSlotsCount();
            if (slotCount > 1)
            {
                inventory.removeSlot(controller.inventoryUI.slots[slotCount - 1].isEmpty);
            }
            if(slotCount == 1 && inventory.items.Count > 0)
            {
                inventory.removeItem(inventory.items[inventory.items.Count - 1]);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<CharacterController2D>() != null)
        {
            canRemoveSlot = false;
            removeSlotTimer = timer;
        }
    }
}