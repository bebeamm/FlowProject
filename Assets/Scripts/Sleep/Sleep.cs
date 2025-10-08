using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Sleep : MonoBehaviour
{
    [SerializeField] private Image fade;
    public void OnSleep()
    {
        TimeManager.Instance.NextDay();

        var sequence = DOTween.Sequence();
        sequence.Append(fade.DOFade(1, 1));
        sequence.Append(fade.DOFade(0, 1));
    }
}
