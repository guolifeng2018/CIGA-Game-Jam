using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHoleInteraction : InteractionScript
{
    public AintAttack m_aintAttack;
    public float m_x = -1.77f;
    public float m_y = -0.95f;

    public bool m_isClose = false;
    
    protected override void OnStart()
    {
        base.OnStart();
        m_aintAttack.gameObject.SetActive(false);
        
        GlobalEvent.AddEvent("Box_Opened", HandleBlockHoleOpened);
    }

    private void OnDestroy()
    {
        GlobalEvent.RemoveEvent("Box_Opened", HandleBlockHoleOpened);
    }

    private void HandleBlockHoleOpened(params object[] args)
    {
        if (m_isClose)
        {
            return;
        }
        
        m_aintAttack.gameObject.SetActive(true);
        LeanTween.moveLocalX(m_aintAttack.gameObject, m_x, 0.3f).setEaseInOutSine();
        LeanTween.moveLocalY(m_aintAttack.gameObject, m_y, 0.3f).setEaseOutBounce().setOnComplete((o =>
            {
                m_aintAttack.m_attack = true;
            }));
    }
}
