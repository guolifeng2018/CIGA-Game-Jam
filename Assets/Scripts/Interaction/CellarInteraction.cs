using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellarInteraction : InteractionScript
{
    private Animator m_animator;
    private bool m_opened;

    public WeightsInteraction m_ineraction;
    
    protected override void OnStart()
    {
        base.OnStart();

        m_animator = GetComponent<Animator>();
        
        GlobalEvent.AddEvent("Cellar_Open", HandleCellarOpen);
    }

    private void HandleCellarOpen(params object[] args)
    {
        if (!m_opened)
        {
            m_animator.Play("Cellar_Open");
            m_opened = true;
        }
    }

    protected void OnTriggerStay2D(Collider2D other)
    {
        WeightsInteraction weight = other.gameObject.GetComponent<WeightsInteraction>();
        if (weight != null)
        {
            if (m_opened)
            {
                m_animator.Play("Cellar_Close");
                m_opened = false;
            }
        }
        else
        {
            if (!m_opened)
            {
                m_animator.Play("Cellar_Open");
                m_opened = true;
            }
        }
    }
}
