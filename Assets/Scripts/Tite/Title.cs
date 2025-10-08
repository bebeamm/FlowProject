using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [SerializeField] private SceneButtonTrigger sceneButtonTrigger;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button startButton;
    [SerializeField] private Button infoButton;

    private InputAction pressSq;
    private InputAction pressTri;

    private void Awake()
    {
        pressSq = InputSystem.actions.FindAction("Interact/Sq");
        pressTri = InputSystem.actions.FindAction("Interact/Tri");
    }

    private void Update()
    {
        if (pressSq.WasPressedThisFrame())
        {
            OnStart();
        }

        if (pressTri.WasPressedThisFrame())
        {
            OnInfo();
        }
    }

    public void OnStart()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(startButton.transform.DOPunchScale(Vector3.one * -0.5f,0.5f));
        sequence.AppendCallback(() =>
        {
            sceneButtonTrigger.ChangeScene();
        });
    }

    public void OnInfo()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(infoButton.transform.DOPunchScale(Vector3.one * -0.5f, 0.5f));
        sequence.AppendCallback(() =>
        {
            //sceneButtonTrigger.ChangeScene();
            Debug.Log("Info");
        });
    }
}
