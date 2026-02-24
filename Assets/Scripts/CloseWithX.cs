using UnityEngine;
using UnityEngine.InputSystem;

public class CloseWithX : MonoBehaviour
{
    private InputAction pressX;

    [SerializeField] private string sceneName;

    private void Awake()
    {
        pressX = InputSystem.actions.FindAction("Interact/X");
    }

    private void OnEnable()
    {
        pressX?.Enable();
        pressX?.Reset();
    }

    private void OnDisable()
    {
        pressX?.Disable();
    }

    private void Update()
{
    if (pressX != null && pressX.WasPressedThisFrame())
    {
        if (SceneLoader.Instance != null && !string.IsNullOrEmpty(sceneName))
        {
            SceneLoader.Instance.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("SceneLoader หรือ sceneName ยังไม่ได้ตั้งค่า");
        }
    }
}
}