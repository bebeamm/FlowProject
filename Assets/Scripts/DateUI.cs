using TMPro;
using UnityEngine;

public class DateUI : MonoBehaviour
{
    public TextMeshProUGUI dateText;

    void Update()
    {
        if (TimeManager.Instance != null)
        {
            dateText.text = "Day" + TimeManager.Instance.GetDay;
        }
    }
}
