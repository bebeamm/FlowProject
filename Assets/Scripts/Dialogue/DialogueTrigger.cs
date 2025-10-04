using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueDayList dialogue;

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogueDay(dialogue);
    }

    //public DialoguePanel dialoguePanel;
    //public void TriggerDialogue()
    //{
    //    DialogueManager.Instance.StartDialogue(dialogue);
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.tag == "Player")
    //    {
    //        TriggerDialogue();
    //    }
    //}
}
 
[System.Serializable]
public class DialogueCharacter
{
    public string name;
    public Sprite icon;
}
 
[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter character;
    [TextArea(3, 10)]
    public string line;
}
