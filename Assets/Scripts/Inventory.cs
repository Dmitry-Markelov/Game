using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public class InventorySlot
    {
        public InventoryItem InventoryItem;
        public int quantity;

        public InventorySlot(InventoryItem InventoryItem, int quantity)
        {
            this.InventoryItem = InventoryItem;
            this.quantity = quantity;
        }
    }

    public List<InventorySlot> slots = new List<InventorySlot>();
    public ItemDatabase itemDatabase;
    private Hotbar hotbar;

    private void Awake()
    {
        hotbar = FindAnyObjectByType<Hotbar>();
    }

    public bool AddItemById(int id, int quantity)
    {
        InventoryItem itemToAdd = itemDatabase.GetItemById(id);

        if (itemToAdd == null)
        {
            Debug.LogWarning("������� � ID " + id + " �� ������!");
            return false;
        }

        foreach (var slot in slots)
        {
            if (slot.InventoryItem != null && slot.InventoryItem.id == id)
            {
                if (slot.quantity + quantity <= itemToAdd.maxStack)
                {
                    slot.quantity += quantity;
                    hotbar.UpdateInventory();
                    return true;
                }
                else
                {
                    Debug.LogWarning("�� ������� ����� � ��������� ��� " + quantity + " ������ �������� " + itemToAdd.name);
                    return false;
                }
            }
        }

        hotbar.UpdateInventory();
        return true;
    }

    public bool DeleteItemById(int id, int quantity)
    {
        Debug.Log("dskfjsd");
        InventoryItem itemToDelete = itemDatabase.GetItemById(id);

        if (itemToDelete == null)
        {
            Debug.LogWarning("������� � ID " + id + " �� ������!");
            return false;
        }

        foreach(var slot in slots)
        {
            if (slot.InventoryItem != null && slot.InventoryItem.id == id)
            {
                if (slot.quantity > 0)
                {
                    slot.quantity -= quantity;
                    if (slot.quantity < 0) slot.quantity = 0;
                    hotbar.UpdateInventory();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        hotbar.UpdateInventory();
        return true;
    }

    public List<(InventoryItem InventoryItem, int quantity)> GetInventoryItems()
    {
        List<(InventoryItem InventoryItem, int quantity)> itemList = new List<(InventoryItem InventoryItem, int quantity)>();

        foreach (var slot in slots)
        {
            if (slot.InventoryItem != null)
            {
                itemList.Add((slot.InventoryItem, slot.quantity));
            }
        }

        return itemList;
    }
}