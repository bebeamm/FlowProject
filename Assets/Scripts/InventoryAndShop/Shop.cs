using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[System.Serializable]
public class ShopItem
{
    public ItemType itemType;
    public int price;
    public int amount = 5;

    [Header("Slot Images")]
    public Image normalSlotImage;   // ภาพปกติ
    public Image hoverSlotImage;    // ภาพตอนถูกเลือก

    public TMP_Text priceText;
}

public class Shop : MonoBehaviour
{
    public static Shop Instance;

    [Header("Main")]
    [SerializeField] GameObject panel;
    [SerializeField] CanvasGroup canvasGroup;

    [Header("Items")]
    [SerializeField] private List<ShopItem> items;   // ใส่สินค้าแต่ละช่องตรงนี้
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color hoverColor = new Color(0.7f, 0.85f, 1f);
    [SerializeField] private int columnCount = 3;

    private int selectIndex = 0;

    [Header("Confirm")]
    [SerializeField] GameObject confirmPanel;
    [SerializeField] Image confirmButton;
    [SerializeField] Image cancelButton;

    private InputAction pressLeft;
    private InputAction pressRight;
    private InputAction pressUp;
    private InputAction pressDown;
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
        pressUp = InputSystem.actions.FindAction("Interact/Up");
        pressDown = InputSystem.actions.FindAction("Interact/Down");
        pressSqr = InputSystem.actions.FindAction("Interact/Sq");
    }

    private void Start()
    {
        UpdateSelectionVisual();
        UpdatePriceTexts();
    }

    private void Update()
    {
        if (!panel.activeSelf) return;

        if (!confirmPanel.activeSelf)
        {
            if (Pressed(pressLeft, KeyCode.LeftArrow)) MoveLeft();
            if (Pressed(pressRight, KeyCode.RightArrow)) MoveRight();
            if (Pressed(pressUp, KeyCode.UpArrow)) MoveUp();
            if (Pressed(pressDown, KeyCode.DownArrow)) MoveDown();

            if (pressSqr != null && pressSqr.WasPressedThisFrame())
                OpenConfirmPanel();

            if (pressX != null && pressX.WasPressedThisFrame())
                CloseShop();
        }
        else
        {
            if (pressSqr != null && pressSqr.WasPressedThisFrame())
                ConfirmBuy();

            if (pressX != null && pressX.WasPressedThisFrame())
                CancelBuy();
        }
    }

    private bool Pressed(InputAction action, KeyCode key)
    {
        return Input.GetKeyDown(key) || (action != null && action.WasPressedThisFrame());
    }

    #region Navigation

    void MoveRight()
    {
        selectIndex++;
        if (selectIndex >= items.Count)
            selectIndex = 0;

        UpdateSelectionVisual();
    }

    void MoveLeft()
    {
        selectIndex--;
        if (selectIndex < 0)
            selectIndex = items.Count - 1;

        UpdateSelectionVisual();
    }

    void MoveDown()
    {
        selectIndex += columnCount;
        if (selectIndex >= items.Count)
            selectIndex %= columnCount;

        UpdateSelectionVisual();
    }

    void MoveUp()
    {
        selectIndex -= columnCount;
        if (selectIndex < 0)
            selectIndex = items.Count - columnCount + (selectIndex + columnCount);

        UpdateSelectionVisual();
    }

    void UpdateSelectionVisual()
{
    for (int i = 0; i < items.Count; i++)
    {
        bool isSelected = i == selectIndex;

        if (items[i].normalSlotImage != null)
            items[i].normalSlotImage.gameObject.SetActive(!isSelected);

        if (items[i].hoverSlotImage != null)
            items[i].hoverSlotImage.gameObject.SetActive(isSelected);
    }
}

    #endregion

    #region Shop Logic

    public void OpenShop()
{
    panel.SetActive(true);
    canvasGroup.alpha = 0;
    canvasGroup.DOFade(1, 0.5f);

    GameManager.Instance.PlayerController.enabled = false;

    UpdateSelectionVisual();

    // 👇 รีเซ็ต input ป้องกันปุ่มค้าง
    pressSqr?.Reset();
    pressX?.Reset();
}

    public void CloseShop()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(canvasGroup.DOFade(0, 0.5f));
        sequence.AppendCallback(() =>
        {
            panel.SetActive(false);
            GameManager.Instance.PlayerController.enabled = true;
        });
    }

    void OpenConfirmPanel()
    {
        confirmPanel.SetActive(true);
    }

    void ConfirmBuy()
    {
        confirmButton.transform.DOPunchScale(Vector3.one * -0.2f, 0.2f);
        BuyItem(selectIndex);
        confirmPanel.SetActive(false);
    }

   void CancelBuy()
{
    if (cancelButton != null)
        cancelButton.transform.DOPunchScale(Vector3.one * -0.2f, 0.2f);

    if (confirmPanel != null)
        confirmPanel.SetActive(false);
}


    void UpdatePriceTexts()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].priceText != null)
                items[i].priceText.text = items[i].price + " ฿";
        }
    }

    #endregion

    #region Buy

   void BuyItem(int index)
{
    if (index < 0 || index >= items.Count) return;

    if (NewInventoryManager.Instance == null)
    {
        Debug.LogError("InventoryManager is NULL");
        return;
    }

    ShopItem item = items[index];
    var inv = NewInventoryManager.Instance;

    if (inv.RemoveMoney(item.price))
    {
        inv.AddItem(item.itemType, item.amount);
    }
}

    #endregion
}