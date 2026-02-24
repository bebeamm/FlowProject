using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public event Action OnEndDialogue;
 
    //public Canvas canvas;
    //public Camera cameraCanvas;

    public DialoguePanel panel;
    //public Image characterIcon;
    //public TextMeshProUGUI characterName;
    //public TextMeshProUGUI dialogueArea;
 
    //private Queue<DialogueLine> lines;

    private List<DialogueLine> lines;

    [SerializeField] private int DialoguePage = 0;

    public float typingSpeed = 0.2f;
 
    public Animator animator;

    public bool isShopDialogue;

    private InputAction pressLeft;
    private InputAction pressRight;
    private InputAction pressX;
    private InputAction pressSqr;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
 
        lines = new List<DialogueLine>();

        pressX = InputSystem.actions.FindAction("Interact/X");
        pressLeft = InputSystem.actions.FindAction("Interact/Left");
        pressRight = InputSystem.actions.FindAction("Interact/Right");
        pressSqr = InputSystem.actions.FindAction("Interact/Sq");
    }

    //private void validate()
    //{
    //    if(cameraCanvas == null)
    //    {
    //        cameraCanvas = Camera.main;
    //    }

    //    if (canvas.worldCamera == null && cameraCanvas != null)
    //    {
    //        canvas.worldCamera = cameraCanvas;
    //    }
    //}

    private void Update()
    {
        //validate();

        //if (Input.GetKeyDown(KeyCode.JoystickButton1))
        //{
        //    if (!panel.gameObject.activeSelf)
        //        return;

        //    NextDialogue();
        //}

        if (pressRight.WasPressedThisFrame())
        {
            if (!panel.gameObject.activeSelf)
                return;

            NextDialogue();
        }
        else if (pressLeft.WasPressedThisFrame())
        {
            if (!panel.gameObject.activeSelf)
                return;

            previousDialogue();
        }
        else if (pressX.WasPressedThisFrame() && panel.gameObject.activeSelf)
        {
            EndDialogue();
        }

        if(isShopDialogue && pressSqr.WasPressedThisFrame())
        {
            EndDialogue();
            Shop.Instance.OpenShop();
        }
    }

    private void previousDialogue()
    {
        DialoguePage--;

        if (DialoguePage < 0)
        {
            DialoguePage = 0;
            return;
        }

        DisplayNextDialogueLine(lines[DialoguePage]);
    }

    private void NextDialogue()
    {
        DialoguePage++;

        if(DialoguePage >= lines.Count)
        {
            DialoguePage = 0;
            EndDialogue();
            return;
        }

        DisplayNextDialogueLine(lines[DialoguePage]);
    }

    public void StartDialogueDay(DialogueDayList dialogueDayList, bool shopDialogue = false)
{
    if (Shop.Instance.isShopActive)
        return;

    int day = TimeManager.Instance.GetDay;

    isShopDialogue = shopDialogue;

    Dialogue selectedDialogue = null;

    for (int i = 0; i < dialogueDayList.dayLists.Count; i++)
    {
        if (dialogueDayList.dayLists[i].Day == day)
        {
            selectedDialogue = dialogueDayList.dayLists[i].Dialogue;
            break; // เจอแล้วหยุดเลย
        }
    }

    // ถ้าไม่มีวันตรง ใช้ตัวสุดท้ายแทน
    if (selectedDialogue == null && dialogueDayList.dayLists.Count > 0)
    {
        selectedDialogue = dialogueDayList.dayLists[dialogueDayList.dayLists.Count - 1].Dialogue;
    }

    if (selectedDialogue != null)
    {
        StartDialogue(selectedDialogue);
    }
}


    public void StartDialogue(Dialogue dialogue)
    {
        if (dialogue == null || dialogue.dialogueLines.Count <= 0)
            return;

        GameManager.Instance.PlayerController.enabled = false;

        panel.gameObject.SetActive(true);

        animator.Play("show");

        panel.OnShop(isShopDialogue);
 
        //lines.Clear();

        lines = dialogue.dialogueLines;

        DialoguePage = 0;

        DisplayNextDialogueLine(lines[DialoguePage]);

        //foreach (DialogueLine dialogueLine in shopDialogue.dialogueLines)
        //{
        //    lines.Enqueue(dialogueLine);
        //}
 
    }
 
    public void DisplayNextDialogueLine(DialogueLine dialogueLine)
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }
 
        //DialogueLine currentLine = lines.Dequeue();

        panel.SetCharacterSprite(dialogueLine.character.icon);
        panel.SetNameText(dialogueLine.character.name);

        //characterIcon.sprite = currentLine.character.icon;
        //characterName.text = currentLine.character.name;
        
        StopAllCoroutines();
 
        StartCoroutine(TypeSentence(dialogueLine));
    }
 
    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        //dialogueArea.text = "";
        panel.SetDialogueLabel("");
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            //dialogueArea.text += letter;
            panel.AddDialogueLabel(letter);
            yield return new WaitForSeconds(typingSpeed);
        }
    }
 
    void EndDialogue()
    {
        OnEndDialogue?.Invoke();
        OnEndDialogue = null;
        isShopDialogue = false;
        GameManager.Instance.PlayerController.enabled = true;
        animator.Play("hide");
    }
}
 