using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class WorkShop : MonoBehaviour
{
    [SerializeField] int step;

    [SerializeField] private TMP_Text paperAmount;
    [SerializeField] private TMP_Text paperBagAmount;
    [SerializeField] private TMP_Text tapeAmount;

    [SerializeField] List<SpriteRenderer> paperSpriteList;

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

        if (InventoryManager.Instance.GetPaper > 0)
        {
            paperSpriteList[0].DOFade(1, 1f);
        }
    }

    private void SetAmount()
    {
        Debug.Log(InventoryManager.Instance.GetTape.ToString());

        paperAmount.text = InventoryManager.Instance.GetPaper.ToString();
        paperBagAmount.text = InventoryManager.Instance.GetPaperBag.ToString();
        tapeAmount.text = InventoryManager.Instance.GetTape.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            NexStep();
        }

        InputByStep();
    }

    public void NexStep()
    {
        if (InventoryManager.Instance.GetTape <= 0 || InventoryManager.Instance.GetPaper <= 0|| isReset)
            return;


        if (step + 1 >= paperSpriteList.Count)
        {
            //step = 0;
            Debug.Log("Out of Range");
            return;
        }

        paperSpriteList[step].DOFade(0,1f);
        paperSpriteList[step + 1].DOFade(1, 1f);

        step ++;

        if (step + 1 >= paperSpriteList.Count)
        {
            Invoke(nameof(ReSetToFirst), 1f);                     
            Done();      
        }

    }

    public void ReSetToFirst()
    {
        if(InventoryManager.Instance.GetPaper > 0)
        {
            paperSpriteList[0].DOFade(1,1f);
            paperSpriteList[paperSpriteList.Count - 1].DOFade(0, 1f);
        }
        else
        {
            paperSpriteList[paperSpriteList.Count - 1].DOFade(0, 1f);
        }

        isReset = false;
    }

    private void Done()
    {
        InventoryManager.Instance.RemovePaper(1);
        InventoryManager.Instance.RemoveTape(1);
        InventoryManager.Instance.AddPaperBag(1);
        step = 0;
        SetAmount();
        isReset = true;
        Debug.Log("Done");
    }
    
    public void InputByStep()
    {
        switch (step)
        {
            case 0: if(Input.GetKeyDown(KeyCode.LeftArrow) || pressLeft.WasPerformedThisFrame()) NexStep();
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.RightArrow) || pressRight.WasPerformedThisFrame()) NexStep();
                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.UpArrow) || pressUp.WasPerformedThisFrame()) NexStep();
                break;
            case 3:
                if (Input.GetKeyDown(KeyCode.LeftArrow) || pressLeft.WasPerformedThisFrame()) NexStep();
                break;
            case 4:
                if (Input.GetKeyDown(KeyCode.RightArrow)|| pressRight.WasPerformedThisFrame()) NexStep();
                break;
            case 5:
                if (Input.GetKeyDown(KeyCode.UpArrow) || pressUp.WasPerformedThisFrame()) NexStep();
                break;
            case 6:
                if (Input.GetKeyDown(KeyCode.DownArrow) || pressDown.WasPerformedThisFrame()) NexStep();
                break;
        }
    }
}
