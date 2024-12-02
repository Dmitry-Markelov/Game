using UnityEngine;

public class LootObject : MonoBehaviour
{
    private Inventory inventory;

    public enum LootType { Cave, AbandonedHouse, Trader }
    public LootType lootType;

    private void Awake()
    {
        inventory = FindAnyObjectByType<Inventory>();
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
        else if (lootType == LootType.Trader)
        {
            //������ ��������
        }

        Destroy(gameObject);
    }
}