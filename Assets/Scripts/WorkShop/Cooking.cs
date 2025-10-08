using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cooking : MonoBehaviour
{
    [SerializeField] private GameObject panStartCook;
    [SerializeField] private GameObject potStartCook;

    [SerializeField] private Animator panAnimator;
    [SerializeField] private Animator potAnimator;

    [SerializeField] private SpriteRenderer gasPan;
    [SerializeField] private SpriteRenderer gasPot;

    [SerializeField] private TMP_Text eggAmountLabel;
    [SerializeField] private TMP_Text pandanAmountLabel;

    [SerializeField] private SceneButtonTrigger SceneButtonTrigger;

    private InputAction pressLeft;
    private InputAction pressRight;
    private InputAction pressX;

    private bool panIsCooking;
    private bool potIsCooking;

    private void Awake()
    {
        pressLeft = InputSystem.actions.FindAction("Interact/Left");
        pressRight = InputSystem.actions.FindAction("Interact/Right");
        pressX = InputSystem.actions.FindAction("Interact/X");

        SetAmount();
    }

    private void Update()
    {
        if (pressLeft.WasPressedThisFrame())
        {
            CookPan();
        }

        if (pressRight.WasPressedThisFrame())
        {
            CookPot();
        }

        if(pressX.WasPressedThisFrame() && !panIsCooking && !potIsCooking)
        {
            SceneButtonTrigger.ChangeScene();
        }
    }

    private void SetAmount()
    {
        eggAmountLabel.text = InventoryManager.Instance.GetEgg.ToString();
        pandanAmountLabel.text = InventoryManager.Instance.GetPandan.ToString();
    }

    private void CookPan()
    {
        panIsCooking = true;
        panStartCook.SetActive(false);
        panAnimator.SetBool("cook", true);
        gasPan.transform.DORotate(new Vector3(0, 0, 1) * 90, 0.5f);
        InventoryManager.Instance.RemoveEgg(1);
        SetAmount();

        Invoke(nameof(CompleteCookPan), 15f);
    }

    private void CompleteCookPan()
    {
        panIsCooking = false;
        panStartCook.SetActive(true);
        panAnimator.SetBool("cook", false);
        gasPan.transform.DORotate(Vector3.zero, 0.5f);
    }

    private void CookPot()
    {
        potIsCooking = true;
        potStartCook.SetActive(false);
        potAnimator.SetBool("cook", true);
        gasPot.transform.DORotate(new Vector3(0, 0, -1) * 90, 0.5f);
        InventoryManager.Instance.RemovePandan(1);
        SetAmount();

        Invoke(nameof(CompleteCookPot), 25f);
    }

    private void CompleteCookPot()
    {
        potIsCooking = false;
        potStartCook.SetActive(true);
        potAnimator.SetBool("cook", false);
        gasPot.transform.DORotate(Vector3.zero, 0.5f);
    }

}
