using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class CookingMenuSelector : MonoBehaviour
{
    public List<GameObject> menuItems;    // ลาก Mungbean / Stickyrice / Layercake
    public List<string> sceneNames;       // ใส่ชื่อซีนของแต่ละเมนู

    public InputActionReference moveAction;
    public InputActionReference submitAction;

    private int currentIndex = 0;
    private float inputCooldown = 0.2f;
    private float lastInputTime;

    void Start()
    {
        UpdateHover();
    }

    void OnEnable()
    {
        moveAction.action.Enable();
        submitAction.action.Enable();
        submitAction.action.performed += OnSubmit;
    }

    void OnDisable()
    {
        submitAction.action.performed -= OnSubmit;
    }

    void Update()
    {
        Vector2 move = moveAction.action.ReadValue<Vector2>();

        if (Time.time - lastInputTime > inputCooldown)
        {
            if (move.x > 0.5f)
            {
                MoveRight();
            }
            else if (move.x < -0.5f)
            {
                MoveLeft();
            }
        }
    }

    void MoveRight()
    {
        currentIndex++;
        if (currentIndex >= menuItems.Count)
            currentIndex = 0;

        lastInputTime = Time.time;
        UpdateHover();
    }

    void MoveLeft()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = menuItems.Count - 1;

        lastInputTime = Time.time;
        UpdateHover();
    }

    void UpdateHover()
    {
        for (int i = 0; i < menuItems.Count; i++)
        {
            Transform hover = menuItems[i].transform.Find("hover");
            if (hover != null)
                hover.gameObject.SetActive(i == currentIndex);
        }
    }


    void OnSubmit(InputAction.CallbackContext context)
    {
    if (PlayerManager.Instance != null)
    {
        PlayerManager.Instance.SetPlayerActive(false);
    }

    SceneLoader.Instance.StartSceneTransition(
    sceneNames[currentIndex],
    Vector2.zero,   // หรือใส่จุดเกิดที่ต้องการ
    false           // จะ active player ไหม
);
}
}