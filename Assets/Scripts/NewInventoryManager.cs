using System.Collections.Generic;
using UnityEngine;

public class NewInventoryManager : MonoBehaviour
{
    public static NewInventoryManager Instance;

    [Header("Start Settings")]
    [SerializeField] private int startMoney = 100;

    [System.Serializable]
    public class DefaultItem
    {
        public ItemType itemType;
        public int amount;
    }

    [SerializeField] private List<DefaultItem> defaultItems;

    private int money;
    private Dictionary<ItemType, int> items = new Dictionary<ItemType, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // ตั้งค่าเริ่มต้นทุกอย่างเป็น 0 ก่อน
        foreach (ItemType type in System.Enum.GetValues(typeof(ItemType)))
        {
            items[type] = 0;
        }

        // เงินเริ่มต้น
        money = startMoney;

        // ของเริ่มต้น
        foreach (var item in defaultItems)
        {
            items[item.itemType] = item.amount;
        }
    }

    public int GetMoney() => money;

    public bool RemoveMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            return true;
        }
        return false;
    }

    public void AddMoney(int amount)
    {
        money += amount;
    }

    public int GetItem(ItemType type)
    {
        return items[type];
    }

    public void AddItem(ItemType type, int amount)
    {
        items[type] += amount;
    }

    public bool RemoveItem(ItemType type, int amount)
    {
        if (items[type] >= amount)
        {
            items[type] -= amount;
            return true;
        }
        return false;
    }
}

public enum ItemType
{
    Egg,
    CoconutMilk,
    Sugar,
    Salt,
    Bean,
    Jelly,
    StickyRiceCustard,
    MungBean,
    LayerCake,
    Pandan
}