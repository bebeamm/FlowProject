using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;

    private void Update()
    {
        if (NewInventoryManager.Instance != null)
        {
            moneyText.text = NewInventoryManager.Instance.GetMoney().ToString();
        }
    }
}