using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WorkShop : MonoBehaviour
{
    [SerializeField] private List<Recipe> recipes;
    [SerializeField] private int currentRecipeIndex;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private int step;

    private InputAction pressUp;
    private InputAction pressDown;
    private InputAction pressLeft;
    private InputAction pressRight;

    private Recipe CurrentRecipe => recipes[currentRecipeIndex];

    private void Awake()
    {
        pressDown = InputSystem.actions.FindAction("Interact/Down");
        pressUp = InputSystem.actions.FindAction("Interact/Up");
        pressLeft = InputSystem.actions.FindAction("Interact/Left");
        pressRight = InputSystem.actions.FindAction("Interact/Right");
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (step >= CurrentRecipe.steps.Count)
            return;

        StepData currentStep = CurrentRecipe.steps[step];
        StepKey? pressedKey = GetPressedKey();

        if (pressedKey == null)
            return;

        if (pressedKey == currentStep.requiredKey)
        {
            StepSuccess();
        }
        else
        {
            Debug.Log("Wrong Step! Reset!");
            ResetMiniGame();
        }
    }

    private StepKey? GetPressedKey()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || (pressLeft != null && pressLeft.WasPerformedThisFrame()))
            return StepKey.Left;

        if (Input.GetKeyDown(KeyCode.RightArrow) || (pressRight != null && pressRight.WasPerformedThisFrame()))
            return StepKey.Right;

        if (Input.GetKeyDown(KeyCode.UpArrow) || (pressUp != null && pressUp.WasPerformedThisFrame()))
            return StepKey.Up;

        if (Input.GetKeyDown(KeyCode.DownArrow) || (pressDown != null && pressDown.WasPerformedThisFrame()))
            return StepKey.Down;

        return null;
    }

    private void StepSuccess()
    {
        // เปลี่ยน sprite ตาม step
        if (step < CurrentRecipe.stepSprites.Count)
        {
            spriteRenderer.sprite = CurrentRecipe.stepSprites[step];
        }

        step++;

        if (step >= CurrentRecipe.steps.Count)
        {
            if (CanCraft())
            {
                Craft();
            }

            ResetMiniGame();
        }
    }

    private bool CanCraft()
    {
        var inv = NewInventoryManager.Instance;

        foreach (var ingredient in CurrentRecipe.ingredients)
        {
            if (inv.GetItem(ingredient.itemType) < ingredient.amount)
                return false;
        }

        return true;
    }

    private void Craft()
    {
        var inv = NewInventoryManager.Instance;

        // หักของก่อน
        foreach (var ingredient in CurrentRecipe.ingredients)
        {
            inv.RemoveItem(ingredient.itemType, ingredient.amount);
        }

        // ให้ของผลลัพธ์
        inv.AddItem(CurrentRecipe.resultItem, CurrentRecipe.resultAmount);

        Debug.Log("Craft Success!");
    }

   private void ResetMiniGame()
{
    step = 0;

    if (spriteRenderer != null && CurrentRecipe.baseSprite != null)
    {
        spriteRenderer.sprite = CurrentRecipe.baseSprite;
    }
}
}