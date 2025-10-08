using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Diary : MonoBehaviour
{
    [SerializeField] private int diaryIndex;
    [SerializeField] private List<DiaryDay> diaryDays;
    [SerializeField] private List<SpriteRenderer> spriteRenderers;

    //private InputAction pressX;
    private InputAction pressLeft;
    private InputAction pressRight;

    private void Awake()
    {
        //pressX = InputSystem.actions.FindAction("Interact/X");
        pressLeft = InputSystem.actions.FindAction("Interact/Left");
        pressRight = InputSystem.actions.FindAction("Interact/Right");

        AddSpriteRenderer();
    }

    private void Update()
    {
        if (pressLeft.WasPressedThisFrame())
        {
            previousDiary();
        }

        if (pressRight.WasPressedThisFrame())
        {
            nextDiary();
        }
    }

    private void previousDiary()
    {
        diaryIndex--;

        if(diaryIndex < 0)
        {
            diaryIndex = 0;
            return;
        }

        Open(diaryIndex);

    }
    private void nextDiary()
    {
        diaryIndex++;

        if(diaryIndex >= spriteRenderers.Count)
        {
            diaryIndex = spriteRenderers.Count - 1;
            return;
        }

        Open(diaryIndex);
    }

    private void AddSpriteRenderer()
    {
        foreach (var item in diaryDays)
        {
            var day = TimeManager.Instance.GetDay;

            if (item.Day <= day)
                spriteRenderers.Add(item.spriteRenderer);
        }
    }

    public void Open(int index)
    {
        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            if (i == diaryIndex)
                spriteRenderers[i].DOFade(1,0.5f);

            if (i != diaryIndex && spriteRenderers[i].color.a > 0)
                spriteRenderers[i].DOFade(0, 0.5f);
        }
    }
}

[Serializable]
public class DiaryDay
{
    public int Day;
    public SpriteRenderer spriteRenderer;
}
