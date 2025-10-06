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

    public bool isDialogueActive = false;
 
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
        else if (pressX.WasPressedThisFrame())
        {
            EndDialogue();
        }

        if(isShopDialogue && pressSqr.WasPressedThisFrame())
        {
            EndDialogue();
            ShopPanel.Instance.OpenShop();
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
            //DialoguePage = 0;
            //EndDialogue();
            return;
        }

        DisplayNextDialogueLine(lines[DialoguePage]);
    }

    public void StartDialogueDay(DialogueDayList dialogueDayList,bool dialogue = false)
    {
        if (ShopPanel.Instance.isShopActive)
            return;

        int day = GameTimeManager.Instance.GetDay;

        isShopDialogue = dialogue;

        for (int i = 0; i < dialogueDayList.dayLists.Count; i++)
        {
            if (dialogueDayList.dayLists[i].Day == day)
            {
                StartDialogue(dialogueDayList.dayLists[i].Dialogue);
            }
            else if(dialogueDayList.dayLists[i].Day > day)
            {
                StartDialogue(dialogueDayList.dayLists[i-1].Dialogue);
            }
            else
            {
                StartDialogue(dialogueDayList.dayLists[dialogueDayList.dayLists.Count-1].Dialogue);
            }
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (dialogue == null || dialogue.dialogueLines.Count <= 0)
            return;

        GameManager.Instance.PlayerController.enabled = false;

        isDialogueActive = true;

        panel.gameObject.SetActive(true);

        animator.Play("show");
 
        //lines.Clear();

        lines = dialogue.dialogueLines;

        DialoguePage = 0;

        DisplayNextDialogueLine(lines[DialoguePage]);

        //foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
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
        isDialogueActive = false;
        isShopDialogue = false;
        GameManager.Instance.PlayerController.enabled = true;
        animator.Play("hide");
    }
}
 