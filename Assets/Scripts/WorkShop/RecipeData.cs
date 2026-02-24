using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ingredient
{
    public ItemType itemType;
    public int amount;
}

[System.Serializable]
public class Recipe
{
    public string recipeName;
    public List<Ingredient> ingredients;
    public ItemType resultItem;
    public int resultAmount;
    public Sprite baseSprite;

    public List<StepData> steps;   // เปลี่ยนจาก stepPattern
    
    [Header("Step Visuals")]
    
    public List<Sprite> stepSprites;   // 👈 เพิ่มอันนี้
}