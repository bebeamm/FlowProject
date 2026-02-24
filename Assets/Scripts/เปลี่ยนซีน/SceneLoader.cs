using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    [Header("Fade")]
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;

    private Vector2 nextPlayerPosition;
    private bool setPlayerPosition;
    private bool playerActiveState = true;
    private bool isTransitioning;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoadedSafeSetup;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoadedSafeSetup;
    }

    // ================================
    // โหลดซีนธรรมดา
    // ================================
    public void LoadScene(string sceneName)
    {
        if (isTransitioning) return;

        setPlayerPosition = false;
        playerActiveState = true;
        StartCoroutine(Transition(sceneName));
    }

    // ================================
    // โหลดซีนพร้อมกำหนดตำแหน่ง Player
    // ================================
    public void StartSceneTransition(string sceneName, Vector2 playerPos, bool activePlayer = true)
    {
        if (isTransitioning) return;

        nextPlayerPosition = playerPos;
        setPlayerPosition = true;
        playerActiveState = activePlayer;
        StartCoroutine(Transition(sceneName));
    }

    private IEnumerator Transition(string sceneName)
    {
        isTransitioning = true;

        EnsureFadeImage();

        yield return Fade(1f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
            yield return null;

        if (setPlayerPosition)
            RepositionPlayer(nextPlayerPosition);

        SetPlayerActive(playerActiveState);
        RetargetAllCinemachineCameras();

        yield return Fade(0f);

        isTransitioning = false;
    }

    private void OnSceneLoadedSafeSetup(Scene scene, LoadSceneMode mode)
    {
        RetargetAllCinemachineCameras();
    }

    // ================================
    // Helpers
    // ================================

    private void RepositionPlayer(Vector2 pos)
    {
        Transform player = FindPlayerTransform();
        if (player == null) return;

        player.position = pos;

        var rb = player.GetComponent<Rigidbody2D>();
        if (rb) rb.velocity = Vector2.zero;
    }

    private void SetPlayerActive(bool state)
    {
        GameObject player = FindPlayer();
        if (player != null)
            player.SetActive(state);
    }

    private void RetargetAllCinemachineCameras()
    {
        Transform player = FindPlayerTransform();
        if (player == null) return;

        var vcams = FindObjectsOfType<CinemachineVirtualCamera>(true);
        foreach (var vcam in vcams)
        {
            vcam.Follow = player;
        }

        EnsureCinemachineBrainOnMainCamera();
    }

    private Transform FindPlayerTransform()
    {
        if (GameManager.Instance == null) return null;
        GameObject playerGO = GameManager.Instance.GetPlayer;
        return playerGO ? playerGO.transform : null;
    }

    private GameObject FindPlayer()
    {
        if (GameManager.Instance == null) return null;
        return GameManager.Instance.GetPlayer;
    }

    private void EnsureCinemachineBrainOnMainCamera()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        if (!cam.GetComponent<CinemachineBrain>())
            cam.gameObject.AddComponent<CinemachineBrain>();
    }

    private void EnsureFadeImage()
    {
        if (fadeImage != null) return;

        var canvasGO = new GameObject("FadeCanvas (Auto)");
        DontDestroyOnLoad(canvasGO);

        var canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = short.MaxValue;

        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();

        var imgGO = new GameObject("FadeImage");
        imgGO.transform.SetParent(canvasGO.transform, false);
        fadeImage = imgGO.AddComponent<Image>();
        fadeImage.color = new Color(0, 0, 0, 0);

        var rt = fadeImage.rectTransform;
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

        fadeImage.raycastTarget = false;
    }

    private IEnumerator Fade(float targetAlpha)
    {
        if (fadeImage == null) yield break;

        float startAlpha = fadeImage.color.a;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            float a = Mathf.Lerp(startAlpha, targetAlpha, t / fadeDuration);
            var c = fadeImage.color;
            c.a = a;
            fadeImage.color = c;
            yield return null;
        }

        var final = fadeImage.color;
        final.a = targetAlpha;
        fadeImage.color = final;
    }
}