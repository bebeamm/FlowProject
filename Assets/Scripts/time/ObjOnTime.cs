using UnityEngine;

public class ObjOnTime : MonoBehaviour
{
    [SerializeField] private TimeOfDay TimeSpawn;

    [SerializeField] private GameObject obj;

    private void Update()
    {
        if(TimeManager.Instance.GetTimeOfDay == TimeSpawn)
        {
            obj.SetActive(true);
        }
        else
        {
            obj.SetActive(false);
        }
    }
}
