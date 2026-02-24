using UnityEngine;

public class InspectTrigger : MonoBehaviour
{  
    public TimeOfDay TimeOfDay;
    public TimeOfDay TimeOfDay2;

    public GameObject Buttonvisual;
    public Transform spawnPoint;
    private GameObject currentBoard;

    // ===== เพิ่มมาใหม่ =====
    public GameObject outline;
    // ======================

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // ----- เปิดขอบขาว -----
            if (outline != null)
            {
                outline.SetActive(true);
            }

            // ----- logic เดิม -----
            if (Buttonvisual != null 
                && currentBoard == null 
                && (TimeOfDay == TimeManager.Instance.GetTimeOfDay 
                || TimeOfDay2 == TimeManager.Instance.GetTimeOfDay 
                || TimeOfDay == TimeOfDay.none))
            {
                currentBoard = Instantiate(Buttonvisual, spawnPoint.position, Quaternion.identity);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // ----- ปิดขอบขาว -----
            if (outline != null)
            {
                outline.SetActive(false);
            }

            // ----- logic เดิม -----
            if (currentBoard != null)
            {
                Destroy(currentBoard);
                currentBoard = null;
            }
        }
    }
}
