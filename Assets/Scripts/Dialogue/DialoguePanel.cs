using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class DialoguePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text nameLabel;
    [SerializeField] private TMP_Text dialogueLabel;
    [SerializeField] private Image characterSprite;

    public void SetNameText(string message)
    {
        nameLabel.text = message;
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
