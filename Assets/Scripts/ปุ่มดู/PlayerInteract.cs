using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private keyToAction actionKey;
    [SerializeField] private TimeOfDay timeOfDay;
    [SerializeField] private bool isBypassAction = false;

    public GameObject targetUI;
    public DialogueTrigger DialogueTrigger;

    private GameObject player;
    private PlayerController playerController;
    private Rigidbody2D playerRb;
    private Animator playerAnimator;
    private bool playerInside = false;

    private InputAction pressUp;
    private InputAction pressDown;
    private InputAction pressX;


    public UnityEvent Event;

    private void Awake()
    {
        pressDown = InputSystem.actions.FindAction("Interact/Down");
        pressUp = InputSystem.actions.FindAction("Interact/Up");
        pressX = InputSystem.actions.FindAction("Interact/X");

    }

    void Start()
    {
        if (targetUI != null)
        {
            targetUI.SetActive(false);
        }
    }

    public void DebugTest()
    {
        Debug.Log("Check...");
        
    }

    void Update()
    {
        if (CheckKey() && isBypassAction)
        {
            Event.Invoke();
        }

        if (playerInside && (Input.GetKeyDown(KeyCode.Return) || CheckKey()) && CheckTime(timeOfDay))
        {
            Event?.Invoke();

            if (targetUI != null)
            {
                targetUI.SetActive(true);
            }

            if (playerController != null)
            {
                playerController.enabled = false;
            }

            if(DialogueTrigger != null)
            {
                DialogueTrigger.TriggerDialogue();
            }

            if (playerRb != null)
            {
                playerRb.velocity = Vector2.zero;
            }

            if (playerAnimator != null)
            {
                playerAnimator.SetFloat("Speed", 0f);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            if (targetUI != null)
            {
                targetUI.SetActive(false);
            }

            if (playerController != null)
            {
                playerController.enabled = true;
            }
        }
    }

    private bool CheckTime(TimeOfDay timeOfDay)
    {
        if (timeOfDay == TimeOfDay.none)
        {
            return true;
        }else if(timeOfDay != TimeOfDay.none)
        {
            if(GameTimeManager.Instance.GetTimeOfDay == timeOfDay)
                return true;
            else return false;
        }
        else
        {
            return true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            player = other.gameObject;
            playerController = player.GetComponent<PlayerController>();
            playerRb = player.GetComponent<Rigidbody2D>();
            playerAnimator = player.GetComponent<Animator>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;

            if (targetUI != null)
            {
                targetUI.SetActive(false);
            }

            if (playerController != null)
            {
                playerController.enabled = true;
            }
        }
    }

    private bool CheckKey()
    {
        if(pressUp.WasCompletedThisFrame() && actionKey == keyToAction.Up) return true;
        else if (pressDown.WasCompletedThisFrame() && actionKey == keyToAction.Down) return true;
        else if (pressX.WasCompletedThisFrame() && actionKey == keyToAction.X) return true;
        else return false;
    }
}


public enum keyToAction
{
    Down,
    Up,
    Left,
    Right,
    X,
    O,
    Sq,
    Tri
}
