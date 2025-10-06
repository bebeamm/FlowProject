using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    public static ShopPanel Instance;

    [SerializeField] GameObject panel;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TMP_Text moneyLabel;
    [SerializeField] GameObject confirmPanel;
    [SerializeField] Image confirmButton;
    [SerializeField] Image cancelButton;

    [SerializeField] private int selectIndex = 0;

    [SerializeField] private List<Toggle> toggles;

    private InputAction pressLeft;
    private InputAction pressRight;
    private InputAction pressX;
    private InputAction pressSqr;

    public bool isShopActive => panel.activeSelf;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        pressX = InputSystem.actions.FindAction("Interact/X");
        pressLeft = InputSystem.actions.FindAction("Interact/Left");
        pressRight = InputSystem.actions.FindAction("Interact/Right");
        pressSqr = InputSystem.actions.FindAction("Interact/Sq");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || pressLeft.WasPressedThisFrame())
        {
            previousSelect();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || pressRight.WasPressedThisFrame())
        {
            nextSelect();
        }

        if (confirmPanel.activeSelf)
        {
            if (pressSqr.WasPressedThisFrame())
            {
                confirmBuy();
                //buyItem(selectIndex);
            }

            if (pressX.WasPressedThisFrame())
            {
                cancelBuy();
            }
        }
        else
        {
            if (pressSqr.WasPressedThisFrame())
            {
                OpenConfirmPanel();
            }

            if (pressX.WasPressedThisFrame())
            {
                CloseShop();
            }
        }
    }

    public void OpenShop()
    {
        panel.SetActive(true);
        canvasGroup.DOFade(1, 1);
        GameManager.Instance.PlayerController.enabled = false;
        updateMoney();
    }

    private void updateMoney()
    {
        moneyLabel.text = InventoryManager.Instance.GetMoney.ToString();
    }

    public void CloseShop()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(canvasGroup.DOFade(0, 1));

        sequence.AppendCallback(() =>
        {
            panel.SetActive(false);
            GameManager.Instance.PlayerController.enabled = true;
        });
    }

    private void OpenConfirmPanel()
    {
        if (!panel.activeSelf)
            return;

            confirmPanel.SetActive(true);
    }

    private void confirmBuy()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(confirmButton.transform.DOPunchScale(Vector3.one * -0.5f, 0.3f));
        sequence.AppendCallback(() =>
        {
            buyItem(selectIndex);
            confirmPanel.SetActive(false);
        });
    }

    private void cancelBuy()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(cancelButton.transform.DOPunchScale(Vector3.one * -0.5f, 0.3f));
        sequence.AppendCallback(() =>
        {
            confirmPanel.SetActive(false);
        });
    }

    private void nextSelect()
    {
        selectIndex++;

        if (selectIndex >= toggles.Count)
            selectIndex = 0;

        toggles[selectIndex].isOn = true;
    }

    private void previousSelect()
    {
        selectIndex--;
        if(selectIndex < 0)
            selectIndex = toggles.Count - 1;

        toggles[selectIndex].isOn = true;
    }

    private void buyItem(int index)
    {
        switch (index)
        {
            case 0: {
                    InventoryManager.Instance.AddPaper(5);
                    InventoryManager.Instance.RemoveMoney(20);
                    updateMoney();
                    Debug.Log("paper");
            } break;
            case 1: {
                    InventoryManager.Instance.AddEgg(5);
                    InventoryManager.Instance.RemoveMoney(20);
                    updateMoney();
                    Debug.Log("egg"); 
            };break;
            case 2: {
                    InventoryManager.Instance.AddTape(5);
                    InventoryManager.Instance.RemoveMoney(50);
                    updateMoney();
                    Debug.Log("tape"); 
            };break;
        }
    }
}
