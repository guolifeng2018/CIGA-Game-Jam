using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWorld : MonoBehaviour
{
    private CanvasGroup m_group;
    
    private void Start()
    {
        m_group = GetComponent<CanvasGroup>();
    }

    public void OpenTheWorld(Action endCallBack)
    {
        LeanTween.value(1f, 0f, 1f).setEaseInOutSine().setOnUpdate((f => { m_group.alpha = f; })).setOnComplete((o =>
        {
            m_group.alpha = 0f;
            endCallBack();
        }));
    }
}
