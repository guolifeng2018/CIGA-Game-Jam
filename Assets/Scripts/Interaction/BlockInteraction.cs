using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInteraction : InteractionScript
{
    public InteractionScript m_key;
    
    protected override void OnStart()
    {
        base.OnStart();
        m_key.gameObject.SetActive(false);
    }
    
    public override bool TriggerEnterAction()
    {
        base.TriggerEnterAction();
        Character character = FindObjectOfType<Character>();
        if (character.CarryItem != null)
        {
            switch (character.CarryItem.m_type)
            {
                case InteractionType.Knife:
                    TriggerWithKnife(character.CarryItem);
                    return true;
            }
        }

        return false;
    }

    private void TriggerWithKnife(InteractionScript script)
    {
        KnifeInteraction knife = script as KnifeInteraction;
        if (knife != null && knife.KnifeState == KnifeInteraction.EKnifeState.Screwdriver)
        {
            LeanTween.moveLocal(gameObject, new Vector3(1.07f, -0.3f, 0f), 0.3f).setEaseInOutSine().setOnComplete((o =>
            {
                m_key.gameObject.SetActive(true);
            }));
            knife.SetOutLine(false);
            knife.enabled = false;
        }
    }
}
