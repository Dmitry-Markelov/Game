using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class LootObject : MonoBehaviour
{
    private Inventory inventory;
    private Transport transport;

    public enum LootType { 
        Cave, AbandonedHouse,
        Trader,
        FallenTree, Rock,
        Storm
    }
    public LootType lootType;

    private void Awake()
    {
        inventory = FindAnyObjectByType<Inventory>();
        transport = FindAnyObjectByType<Transport>();
    }

    public void Interact()
    {
        if (lootType == LootType.Cave || lootType == LootType.AbandonedHouse)
        {
            LootTable lootTable = GetComponent<LootTable>();
            if (lootTable != null)
            {
                LootItem loot = lootTable.GetRandomLoot();

                bool isAdded = inventory.AddItemById(loot.id, 1);
                if (isAdded)
                {
                    Debug.Log("�� ��������: " + loot.itemName);
                }
                else
                {
                    Debug.Log("�� ������� �������� �������.");
                }
            }
            else
            {
                Debug.LogWarning("LootTable �� ������ �� �������!");
            }
        }
        else if (lootType == LootType.FallenTree || lootType == LootType.Rock)
        {
            int id = (lootType == LootType.FallenTree) ? 5 : 6;
                
            InventoryItem item = inventory.GetItemByID(id);
            if (item != null)
            {
                if(inventory.DeleteItemById(id, 1))
                {
                    transport.inObstacle = false;
                    Destroy(gameObject);
                }
            }
            else
            {
                Debug.Log("�� ������� ����� �������!");
            }
            return;
        }
        else if (lootType == LootType.Trader)
        {
            //������ ��������
        }

        Destroy(gameObject);
    }
}