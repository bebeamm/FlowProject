using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;
    public InputActionReference interactAction;

    private bool isMenuOpen = false;

    void Start()
    {
        menuCanvas.SetActive(false);
    }

    void OnEnable()
    {
        interactAction.action.Enable();
    }

    void OnDisable()
    {
        interactAction.action.Disable();
    }

    void Update()
    {
        if (interactAction.action.WasPressedThisFrame())
        {
            ToggleMenu();
        }
    }

    void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        menuCanvas.SetActive(isMenuOpen);

        Debug.Log("Menu Open: " + isMenuOpen);
    }
}