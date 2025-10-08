using System.Collections.Generic;
using UnityEngine;

public class NongBuaManager : MonoBehaviour
{
    public static NongBuaManager Instance;

    [SerializeField] private GameObject nongBua;
    [SerializeField] private Animator animator;

    [SerializeField] private DialogueDayList canBuyDialogue;
    [SerializeField] private Dialogue cantBuyDialogue;

    [SerializeField] private List<bool> DayCome;
    [SerializeField] bool isCome;

    bool isWaitingTime;
    int countOfWaiting;
    float timeWaiting;
 
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        //DayCome = new List<bool>();
        RandomizeTrue(DayCome, 20);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("OutTheWay");
        }

        if (isWaitingTime)
        {
            timeWaiting += Time.deltaTime;
            if (timeWaiting >= 10)
            {
                timeWaiting = 0;
                countOfWaiting++;
                // play sound
                Debug.Log("Waiting... " + countOfWaiting);
                if(countOfWaiting >= 3)
                {
                    ComeOut();
                }
            }
        }

        CheckDayCome();
    }

    private void CheckDayCome()
    {
        if(TimeManager.Instance.GetTimeCount >= 165f && DayCome[TimeManager.Instance.GetDay - 1] && !isCome)
        {
            ComeIn();
        }
    }

    public void TryToSellPaperBag()
    {
        restWaiting();
        if (InventoryManager.Instance.GetPaperBag > 0)
        {
            Debug.Log("sell PaperBag");
            DialogueManager.Instance.OnEndDialogue += SellPaperBag;
            DialogueManager.Instance.StartDialogueDay(canBuyDialogue);
        }
        else
        {
            Debug.Log("Cant sell");
            DialogueManager.Instance.OnEndDialogue += ComeOut;
            DialogueManager.Instance.StartDialogue(cantBuyDialogue);
        }

        InventoryManager.Instance.AddPaper(20);
        InventoryManager.Instance.AddTape(5);
    }

    public void EndComeIn()
    {
        isWaitingTime = true;
    }

    public void EndComeOut()
    {
        // Sound pause
    }

    public void ComeIn()
    {
        isCome = true;
        animator.SetTrigger("ComeIn");

        // Sound play
    }

    private void restWaiting()
    {
        isWaitingTime = false;
        countOfWaiting = 0;
        timeWaiting = 0;
    }

    public void ComeOut()
    {
        isCome = false;
        restWaiting();
        animator.SetTrigger("ComeOut");
    }

    public void SellPaperBag()
    {
        int amountPaperBag = InventoryManager.Instance.GetPaperBag;

        if (amountPaperBag <= 0)
            return;

        InventoryManager.Instance.AddMoney(amountPaperBag * 10);
        InventoryManager.Instance.RemovePaperBag(amountPaperBag);
        animator.SetTrigger("OutTheWay");
        Debug.Log("Complete Sell");

    }

    public static void RandomizeTrue(List<bool> boolList, int countToTrue)
    {
        if (boolList == null || boolList.Count == 0)
        {
            Debug.LogWarning("List is empty or null.");
            return;
        }

        // ป้องกันจำนวนเกินขนาด list
        countToTrue = Mathf.Clamp(countToTrue, 0, boolList.Count);

        // รีเซ็ตทั้งหมดเป็น false ก่อน (ถ้าต้องการ)
        for (int i = 0; i < boolList.Count; i++)
            boolList[i] = false;

        // สร้างรายการ index ทั้งหมด
        List<int> availableIndices = new List<int>();
        for (int i = 0; i < boolList.Count; i++)
            availableIndices.Add(i);

        // สุ่ม index แล้วตั้งเป็น true
        for (int i = 0; i < countToTrue; i++)
        {
            int randomIndex = Random.Range(0, availableIndices.Count);
            int chosen = availableIndices[randomIndex];
            boolList[chosen] = true;
            availableIndices.RemoveAt(randomIndex); // เอาออกเพื่อไม่ให้ซ้ำ
        }
    }
}
