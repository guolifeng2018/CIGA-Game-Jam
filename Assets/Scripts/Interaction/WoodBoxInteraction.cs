using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBoxInteraction : InteractionScript
{
    private bool m_opened;
    public Sprite m_openBoxSprite;
    public InteractionScript m_key;
    protected override void OnStart()
    {
        base.OnStart();
        
        m_key.gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        WeightsInteraction weightsInteraction = other.GetComponent<WeightsInteraction>();
        if (weightsInteraction != null && !m_opened)
        {
            m_opened = true;
            m_render.sprite = m_openBoxSprite;

            m_key.gameObject.SetActive(true);
            m_canShowOutline = false;

            Character character = FindObjectOfType<Character>();
            character.PlayAudio("wood_broken");
        }
    }
}
