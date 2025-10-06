using System;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField] private int money;
    [SerializeField] private Item egg;
    [SerializeField] private Item paper;
    [SerializeField] private Item tape;
    [SerializeField] private Item paperBag;

    public int GetMoney => money;
    public int GetEgg => egg.Amount; 
    public int GetPaper => paper.Amount; 
    public int GetTape => tape.Amount; 
    public int GetPaperBag => paperBag.Amount;

    public void AddMoney(int amount) => money += amount;
    public void AddEgg(int amount) => egg.Amount += amount;
    public void AddPaper(int amount) => paper.Amount += amount;
    public void AddTape(int amount) => tape.Amount += amount;

    public void RemoveMoney(int amount) => money -= amount;
    public void RemoveEgg(int amount) => egg.Amount -= amount;
    public void RemovePaper(int amount) => paper.Amount -= amount;
    public void RemoveTape(int amount) => tape.Amount -= amount;
    public void AddPaperBag(int amount) => paperBag.Amount += amount;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        SetDefault();
    }

    public void SetDefault()
    {
        //egg.Amount = 5;
        //paper.Amount = 2;
        //tape.Amount = 1;
        //paperBag.Amount = 0;
    }
    
}

[Serializable]
public class Item
{
    public int Amount;
}

