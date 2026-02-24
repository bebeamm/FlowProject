using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TabController : MonoBehaviour
{    
    public Image[] tabImages;
    public GameObject[] pages;

    private int currentTab = 0;

    void Start()
    {
        ActivateTab(currentTab);
    }

    void Update()
    {
        if (Gamepad.current == null) return;

        // กดขวา
        if (Gamepad.current.dpad.right.wasPressedThisFrame)
        {
            currentTab++;
            if (currentTab >= pages.Length)
                currentTab = 0;

            ActivateTab(currentTab);
        }

        // กดซ้าย
        if (Gamepad.current.dpad.left.wasPressedThisFrame)
        {
            currentTab--;
            if (currentTab < 0)
                currentTab = pages.Length - 1;

            ActivateTab(currentTab);
        }
    }

    public void ActivateTab(int tabNo)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
            tabImages[i].color = Color.gray;
        }

        pages[tabNo].SetActive(true);
        tabImages[tabNo].color = Color.white;
    }
}
