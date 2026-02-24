using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class StickyRiceWorkshop : MonoBehaviour
{
    [SerializeField] int step;

    [Header("UI Amount")]
    [SerializeField] private TMP_Text EggAmount;
    [SerializeField] private TMP_Text CoconutMilkAmount;
    [SerializeField] private TMP_Text SugarAmount;
    [SerializeField] private TMP_Text SaltAmount;
    [SerializeField] private TMP_Text StickyRiceCustardAmount;

    [Header("Step Sprites")]
    [SerializeField] List<SpriteRenderer> StepSpriteList;

    bool isReset;

    private InputAction pressUp;
    private InputAction pressDown;
    private InputAction pressLeft;
    private InputAction pressRight;

    private void Awake()
    {
        pressDown = InputSystem.actions.FindAction("Interact/Down");
        pressUp = InputSystem.actions.FindAction("Interact/Up");
        pressLeft = InputSystem.actions.FindAction("Interact/Left");
        pressRight = InputSystem.actions.FindAction("Interact/Right");

        SetAmount();

        if (NewInventoryManager.Instance.GetItem(ItemType.Egg) > 0)
        {
            StepSpriteList[0].DOFade(1, 1f);
            StepSpriteList[0].gameObject.SetActive(true);
        }
    }

    private void SetAmount()
    {
        EggAmount.text = NewInventoryManager.Instance.GetItem(ItemType.Egg).ToString();
        CoconutMilkAmount.text = NewInventoryManager.Instance.GetItem(ItemType.CoconutMilk).ToString();
        SugarAmount.text = NewInventoryManager.Instance.GetItem(ItemType.Sugar).ToString();
        SaltAmount.text = NewInventoryManager.Instance.GetItem(ItemType.Salt).ToString();
        StickyRiceCustardAmount.text = NewInventoryManager.Instance.GetItem(ItemType.StickyRiceCustard).ToString();
    }

    private void Update()
    {
        InputByStep();
    }

    public void NextStep()
{
    if (isReset) return;

    // ถ้าถึงขั้นสุดท้ายแล้ว
    if (step >= StepSpriteList.Count - 1)
    {
        Done();
        Invoke(nameof(ResetToFirst), 1f);
        return;
    }

    // ปิด sprite ปัจจุบัน
    StepSpriteList[step].DOFade(0, 0.5f);
    StartCoroutine(DelayActive(StepSpriteList[step].gameObject, false, 0.5f));

    step++;

    // เปิด sprite ถัดไป
    StepSpriteList[step].gameObject.SetActive(true);
    StepSpriteList[step].DOFade(1, 0.5f);
}

    private IEnumerator DelayActive(GameObject obj, bool state, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(state);
    }

    private void Done()
    {
        NewInventoryManager.Instance.AddItem(ItemType.StickyRiceCustard, 5);
        step = 0;
        SetAmount();
        isReset = true;
        Debug.Log("Cook Complete!");
    }

   public void ResetToFirst()
{
    // ปิดทุก sprite
    for (int i = 0; i < StepSpriteList.Count; i++)
    {
        StepSpriteList[i].gameObject.SetActive(false);
    }

    // เปิด sprite 0 (ชามเปล่า)
    StepSpriteList[0].gameObject.SetActive(true);
    StepSpriteList[0].DOFade(1, 0.5f);

    step = 0;
    isReset = false;
}

    public void InputByStep()
    {
        switch (step)
        {
            case 0: // Step1 - Up - Egg 4
                if (pressUp.WasPerformedThisFrame())
                {
                    if (NewInventoryManager.Instance.RemoveItem(ItemType.Egg, 4))
                        NextStep();
                }
                break;

            case 1: // Step2 - Left - CoconutMilk 3
    if (pressLeft.WasPerformedThisFrame())
    {
        if (!NewInventoryManager.Instance.RemoveItem(ItemType.CoconutMilk, 3))
        {
            Debug.Log("CoconutMilk not enough!");
            return;
        }

        NextStep();
    }
    break;

            case 2: // รวม Sugar + Salt
    if (pressRight.WasPerformedThisFrame())
    {
        var inv = NewInventoryManager.Instance;

        if (inv.GetItem(ItemType.Sugar) >= 4 &&
            inv.GetItem(ItemType.Salt) >= 1)
        {
            inv.RemoveItem(ItemType.Sugar, 4);
            inv.RemoveItem(ItemType.Salt, 1);

            NextStep();
        }
    }
    break;

            case 3: // Step5 - Down (no remove)
                if (pressDown.WasPerformedThisFrame())
                {
                    NextStep();
                }
                break;

            case 4: // Step6 - Up (no remove)
                if (pressUp.WasPerformedThisFrame())
                {
                    NextStep();
                }
                break;

            case 5: // Step7 - Left (no remove)
                if (pressLeft.WasPerformedThisFrame())
                {
                    NextStep();
                }
                break;

            case 6: // Step8 - Right (no remove)
                if (pressDown.WasPerformedThisFrame())
                {
                    NextStep();
                }
                break;

            case 7: // Step8 - Left (no remove)
                if (pressLeft.WasPerformedThisFrame())
                {
                    NextStep();
                }
                break;
            case 8: // Step8 - Right (no remove)
                if (pressRight.WasPerformedThisFrame())
                {
                    NextStep();
                }
                break;
        }

        SetAmount();
    }
}