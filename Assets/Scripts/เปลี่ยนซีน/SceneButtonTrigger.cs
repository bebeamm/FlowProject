using System.Collections;
using UnityEngine;

public class SceneButtonTrigger : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerSpawnPosition;

    public GameObject buttonVisual;

    public string submitButtonName = "Submit";
    public float pressCooldown = 0.25f;

    bool playerInside = false;
    bool isActivating = false;
    float lastPressTime = -10f;

    [SerializeField] bool isPlayerActive = true;
    [SerializeField] bool isControlFormOut;

    void Start()
    {
        // ✅ ให้ปุ่มแสดงเลยตั้งแต่เริ่ม
        if (buttonVisual != null)
            buttonVisual.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }

    void Update()
    {
        // ❗ จะกดได้ก็ต่อเมื่ออยู่ใน Trigger เท่านั้น
        if (!playerInside || isActivating || isControlFormOut) return;

        if (IsSubmitPressed())
        {
            if (Time.time - lastPressTime < pressCooldown) return;
            lastPressTime = Time.time;
            StartCoroutine(ActivateAndLoad());
        }
    }

    public void ChangeScene()
    {
        // ถ้าจะให้กดจาก UI Button ต้องเช็คตรงนี้ด้วย
        if (!playerInside) return;

        if (Time.time - lastPressTime < pressCooldown) return;
        lastPressTime = Time.time;
        StartCoroutine(ActivateAndLoad());
    }

    IEnumerator ActivateAndLoad()
    {
        isActivating = true;

        if (SceneLoader.Instance != null)
        {
            SceneLoader.Instance.StartSceneTransition(sceneToLoad, playerSpawnPosition, isPlayerActive);
        }
        else
        {
            Debug.LogWarning("SceneLoader.Instance is null! ไม่พบ SceneLoader");
        }

        isActivating = false;
        yield break;
    }

    bool IsSubmitPressed()
    {
        if (!string.IsNullOrEmpty(submitButtonName) && Input.GetButtonDown(submitButtonName))
            return true;

        if (Input.GetKeyDown(KeyCode.JoystickButton2))
            return true;

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            return true;

        return false;
    }
}