using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField] private GameObject morningLight;
    [SerializeField] private GameObject eveningLight;
    [SerializeField] private GameObject NightLight;

    [SerializeField] private SpriteRenderer sky;

    private void Update()
    {
        SetEnv();
    }

    private void SetEnv()
    {
        var TOD = GameTimeManager.Instance.GetTimeOfDay;

        switch (TOD)
        {
            case TimeOfDay.morning:
            {
                    eveningLight.SetActive(false);
                    NightLight.SetActive(false);
                    morningLight.SetActive(true);

                    if(sky != null)
                        sky.color = Color.white;
            }
            break;
            case TimeOfDay.evening:
            {
                    morningLight.SetActive(false);
                    NightLight.SetActive(false);
                    eveningLight.SetActive(true);

                    if (sky != null)
                        sky.color = Color.white;
            }
            break;
            case TimeOfDay.night:
            {
                    morningLight.SetActive(false);
                    eveningLight.SetActive(false);
                    NightLight.SetActive(true);

                    if (sky != null)
                        sky.color= Color.gray;
                }
            break;
        }
    }
}