using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitingPlayerAlpha : MonoBehaviour
{
    private SpriteRenderer[] renderers;
    private PlayerController controller;

    void Awake()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
        controller = GetComponent<PlayerController>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "waiting")
        {
            SetAlpha(0f);

            if (controller != null)
                controller.enabled = false;
        }
        else
        {
            SetAlpha(1f);

            if (controller != null)
                controller.enabled = true;
        }
    }
void SetAlpha(float value)
{
    foreach (var r in renderers)
    {
        if (r == null) continue;

        Color c = r.color;
        c.a = value;
        r.color = c;
    }
}
}
