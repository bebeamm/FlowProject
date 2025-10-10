using UnityEngine;

public class InspectTrigger : MonoBehaviour
{  
    public TimeOfDay TimeOfDay;
    public TimeOfDay TimeOfDay2;
    public GameObject boardPrefab;
    public Transform spawnPoint;
    private GameObject currentBoard;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (boardPrefab != null && currentBoard == null && (TimeOfDay == TimeManager.Instance.GetTimeOfDay || TimeOfDay2 == TimeManager.Instance.GetTimeOfDay || TimeOfDay == TimeOfDay.none))
            {
                currentBoard = Instantiate(boardPrefab, spawnPoint.position, Quaternion.identity);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (currentBoard != null)
            {
                Destroy(currentBoard);
                currentBoard = null;
            }
        }
    }
}
