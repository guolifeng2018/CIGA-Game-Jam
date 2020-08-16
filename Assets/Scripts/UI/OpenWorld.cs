using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenWorld : MonoBehaviour
{
    private CanvasGroup m_group;
    private Image m_image;
    
    private void Start()
    {
        m_group = GetComponent<CanvasGroup>();
        Image[] images = gameObject.GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; i++)
        {
            if (images[i].name == "Image (1)")
            {
                m_image = images[i];
                break;
            }
        }
        
        m_image.gameObject.SetActive(false);
    }

    public void OpenTheWorld(Action endCallBack)
    {
        LeanTween.value(1f, 0f, 0.3f).setOnComplete((o =>
        {
            m_image.gameObject.SetActive(true);
            Character character = FindObjectOfType<Character>();
            character.PlayAudio("head");
        }));
        LeanTween.value(1f, 0f, 2f).setEaseInOutSine().setOnComplete((o =>
        {
            m_group.alpha = 0f;
            endCallBack();
        })).setDelay(1f);
    }
}
