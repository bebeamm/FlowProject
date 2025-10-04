using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Create Dialogue", fileName ="New Dialogue")]
public class Dialogue : ScriptableObject
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}