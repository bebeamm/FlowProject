using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardWithDay : MonoBehaviour
{
    [SerializeField] private Image boardImage;
    [SerializeField] List<BoardDay> BoardList;

    private void OnEnable()
    {
        SetBoardOnDay();
    }

    private void SetBoardOnDay()
    {
        int day = GameTimeManager.Instance.GetDay;

        for (int i = 0; i < BoardList.Count; i++)
        {
            if (BoardList[i].Day == day)
            {
                boardImage.sprite = BoardList[i].Sprite;
                //StartDialogue(dialogueDayList.dayLists[i].Dialogue);
            }
            else if (BoardList[i].Day > day)
            {
                //StartDialogue(dialogueDayList.dayLists[i - 1].Dialogue);
                boardImage.sprite = BoardList[i - 1].Sprite;
            }
            else
            {
                boardImage.sprite = BoardList[BoardList.Count - 1].Sprite;
                //StartDialogue(dialogueDayList.dayLists[dialogueDayList.dayLists.Count - 1].Dialogue);
            }
        }
    }
}

[Serializable]
public class BoardDay
{
    public int Day;
    public Sprite Sprite;
}