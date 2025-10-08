using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text nameLabel;
    [SerializeField] private TMP_Text dialogueLabel;
    [SerializeField] private Image characterSprite;
    [SerializeField] private Image close;
    [SerializeField] private Image closeShop;

    public void SetNameText(string message)
    {
        nameLabel.text = message;
    }

    public void OnShop(bool state)
    {
        if (state)
        {
            closeShop.gameObject.SetActive(true);
            close.gameObject.SetActive(false);
        }
        else
        {
            closeShop.gameObject.SetActive(false);
            close.gameObject.SetActive(true);
        }
    }

    public void SetDialogueLabel(string message)
    {
        dialogueLabel.text = message;
    }

    public void AddDialogueLabel(char cha)
    {
        dialogueLabel.text += cha;
    }

    public void SetCharacterSprite(Sprite sprite)
    {
        characterSprite.sprite = sprite;
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
