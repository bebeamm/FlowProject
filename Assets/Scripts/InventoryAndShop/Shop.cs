using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private int selectIndex = 0;

    [SerializeField] private List<Toggle> toggles;

    private void Awake()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            previousSelect();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            nextSelect();
        }


        if (Input.GetKeyDown(KeyCode.B))
        {
            buyItem(selectIndex);
        }
    }

    private void nextSelect()
    {
        selectIndex++;

        if (selectIndex >= toggles.Count)
            selectIndex = 0;

        toggles[selectIndex].isOn = true;
    }

    private void previousSelect()
    {
        selectIndex--;
        if(selectIndex < 0)
            selectIndex = toggles.Count - 1;

        toggles[selectIndex].isOn = true;
    }

    private void buyItem(int index)
    {
        switch (index)
        {
            case 0: Debug.Log("Paper"); break;
            case 1: Debug.Log("egg");break;
            case 2: Debug.Log("tape");break;
        }
    }
}
