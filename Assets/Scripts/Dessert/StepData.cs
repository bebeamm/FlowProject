using UnityEngine;

public enum StepKey
{
    Up,
    Down,
    Left,
    Right
}

[System.Serializable]
public class StepData
{
    [Header("Input")]
    public StepKey requiredKey;

    [Header("Animation")]
    public Animator targetAnimator;   // Animator ที่จะสั่ง
    public string triggerName;        // ชื่อ Trigger ใน Animator
}