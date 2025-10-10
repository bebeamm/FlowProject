using UnityEngine;

public class TimeSwitchObject : MonoBehaviour
{
    [SerializeField] private GameObject morningObject;
    [SerializeField] private GameObject eveningObject;
    [SerializeField] private GameObject nightObject;

    private void Update()
    {
        var TOD = TimeManager.Instance.GetTimeOfDay;

        if(TOD == TimeOfDay.morning && morningObject != null)
        {
            morningObject.SetActive(true);
            eveningObject.SetActive(false);
            nightObject.SetActive(false);
        }
        else if(TOD == TimeOfDay.evening && eveningObject != null)
        {
            morningObject.SetActive(false);
            eveningObject.SetActive(true);
            nightObject.SetActive(false);
        }
        else if (TOD == TimeOfDay.night && nightObject != null)
        {
            morningObject.SetActive(false);
            eveningObject.SetActive(false);
            nightObject.SetActive(true);
        }
    }
}