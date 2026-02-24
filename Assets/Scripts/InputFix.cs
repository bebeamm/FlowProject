using UnityEngine;
using UnityEngine.InputSystem;

public class InputFix : MonoBehaviour
{
    void Awake()
    {
        InputSystem.settings.maxEventBytesPerUpdate = 0;
    }
}
