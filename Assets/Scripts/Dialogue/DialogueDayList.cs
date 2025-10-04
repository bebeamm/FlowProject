using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Dialogue Day list", fileName ="New Dialogue Day list")]
public class DialogueDayList : ScriptableObject
{
    public List<DayList> dayLists = new List<DayList>();
}

[Serializable]
public class DayList
{
    public int Day;
    public Dialogue Dialogue;
}
