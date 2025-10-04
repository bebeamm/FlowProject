using System;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField] private Item egg;
    [SerializeField] private Item paper;
    [SerializeField] private Item tape;

    public int GetEgg => egg.Amount; 
    public int GetPaper => paper.Amount; 
    public int GetTape => tape.Amount; 

    public void AddEgg(int amount) => egg.Amount += amount;
    public void AddPaper(int amount) => paper.Amount += amount;
    public void AddTape(int amount) => tape.Amount += amount;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        SetDefault();
    }

    public void SetDefault()
    {
        egg.Amount = 5;
        paper.Amount = 5;
        tape.Amount = 5;
    }
    
}

[Serializable]
public class Item
{
    //public ItemType Type;
    public int Amount;
}

public enum ItemType
{
    Egg,
    Paper,
    Tape
}
