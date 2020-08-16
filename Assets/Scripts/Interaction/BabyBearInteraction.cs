using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyBearInteraction : InteractionScript
{
    public Sprite m_bearOpen;
    public InteractionScript m_book;
    
    protected override void OnStart()
    {
        base.OnStart();
        
        m_book.gameObject.SetActive(false);
    }
    
    public override void TriggerEnterAction()
    {
        base.TriggerEnterAction();
        
        Character character = FindObjectOfType<Character>();
        if (character.CarryItem != null)
        {
            switch (character.CarryItem.m_type)
            {
                case InteractionType.Knife:
                    TriggerWithKnife(character.CarryItem);
                    break;
            }
        }
    }

    private void TriggerWithKnife(InteractionScript script)
    {
        KnifeInteraction knife = script as KnifeInteraction;
        if (knife != null && knife.KnifeState == KnifeInteraction.EKnifeState.Knife)
        {
            m_render.sprite = m_bearOpen;
            
            m_book.gameObject.SetActive(true);
            LeanTween.moveX(m_book.gameObject, transform.position.x-0.2f, 0.3f).setEaseInOutSine();
            LeanTween.moveY(m_book.gameObject, transform.position.y + 0.1f, 0.15f).setEaseInCirc();
            LeanTween.moveY(m_book.gameObject, transform.position.y - 0.1f, 0.15f).setEaseInCirc().setDelay(0.15f);
            m_canPick = false;
            m_canShowOutline = false;
        }
    }
}
