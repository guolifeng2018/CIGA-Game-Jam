using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInteraction : InteractionScript
{
    public Sprite m_boxOpen;
    public InteractionScript m_key;
    
    protected override void OnStart()
    {
        base.OnStart();
    }
    
    public override void TriggerEnterAction()
    {
        base.TriggerEnterAction();
        Character character = FindObjectOfType<Character>();
        if (character.CarryItem != null)
        {
            switch (character.CarryItem.m_type)
            {
                case InteractionType.Casekey:
                    TriggerWithCaseKey(character.CarryItem);
                    break;
            }
        }
    }

    private void TriggerWithCaseKey(InteractionScript script)
    {
        CaseKeyInteraction caseKey = script as CaseKeyInteraction;
        if (caseKey != null)
        {
            m_render.sprite = m_boxOpen;
            script.gameObject.SetActive(false);
            Character character = FindObjectOfType<Character>();
            character.DropCarryItem();

            m_key.gameObject.SetActive(true);
            LeanTween.moveLocalX(m_key.gameObject, -3.05f, 0.5f).setEaseInOutSine();
            LeanTween.moveLocalY(m_key.gameObject, 0.62f, 0.25f).setEaseInCirc();
            LeanTween.moveLocalY(m_key.gameObject, -0.22f, 0.25f).setEaseOutCirc().setDelay(0.25f);
        }
    }
}
