using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Dream : MonoBehaviour
{
    [SerializeField] private VideoPlayer player;
    [SerializeField] private Image fade;
    [SerializeField] private SceneButtonTrigger sceneButtonTrigger;
    [SerializeField] private List<DreamDate> videoClips;

    bool isSceneChange;
    bool isHasClip;


    private void Awake()
    {
        var day = TimeManager.Instance.GetDay;

        foreach (var d in videoClips)
        {
            if(d.day == day)
            {
                player.clip = d.videoClip;
                isHasClip = true;
                return;
            }
        }

        if (!isHasClip)
            waiting();
    }

    private void Update()
    {
        if(!isHasClip)
            return;

        if(player.frame >= player.clip.frameCount - 10f && !isSceneChange)
        {
            isSceneChange = true;
            waiting();
            Debug.Log("End Video")
;       }
    }

    private void waiting()
    {
        fade.DOFade(1,0.5f);
        Invoke(nameof(Sleep), 3f);
    }

    private void Sleep()
    {
        TimeManager.Instance.NextDay();
        sceneButtonTrigger.ChangeScene();
    }
}

[Serializable]
public class DreamDate
{
    public int day;
    public VideoClip videoClip;
}