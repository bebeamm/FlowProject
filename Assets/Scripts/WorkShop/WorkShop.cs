using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WorkShop : MonoBehaviour
{
    [SerializeField] int step;
    //[SerializeField] Transform workSpace;
    [SerializeField] List<SpriteRenderer> paperSpriteList;

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

        if(step + 1 >= paperSpriteList.Count)
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
            Invoke(nameof(ReSetToFirst), 2f);
            step = 0;
            Debug.Log("Done");
        }

    }

    public void ReSetToFirst()
    {
        if(InventoryManager.Instance.GetPaper > 0)
        {
            paperSpriteList[0].DOFade(1,1f);
            paperSpriteList[paperSpriteList.Count - 1].DOFade(0, 1f);
        }
    }
    
    public void InputByStep()
    {
        switch (step)
        {
            case 0: if(Input.GetKeyDown(KeyCode.RightArrow)) NexStep();
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.LeftArrow)) NexStep();
                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.UpArrow)) NexStep();
                break;
            case 3:
                if (Input.GetKeyDown(KeyCode.RightArrow)) NexStep();
                break;
            case 4:
                if (Input.GetKeyDown(KeyCode.LeftArrow)) NexStep();
                break;
            case 5:
                if (Input.GetKeyDown(KeyCode.UpArrow)) NexStep();
                break;
            case 6:
                if (Input.GetKeyDown(KeyCode.DownArrow)) NexStep();
                break;
        }
    }
}
