using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TweenButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public Vector3 m_scale = new Vector3(1.1f, 1.1f, 1.1f);

    public Action m_buttonClick;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        LeanTween.cancelAll();
        LeanTween.scale(gameObject, m_scale, 0.3f).setEaseInOutSine();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        LeanTween.cancelAll();
        LeanTween.scale(gameObject, Vector3.one, 0.3f).setEaseInOutSine();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (m_buttonClick != null)
        {
            m_buttonClick();
        }
    }
}
