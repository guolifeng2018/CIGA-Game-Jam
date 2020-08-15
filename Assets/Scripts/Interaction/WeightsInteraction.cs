using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightsInteraction : InteractionScript
{
    protected override void OnStart()
    {
        base.OnStart();
        
    }
    
    public override void DropDownItem(Transform character)
    {
        LeanTween.moveY(gameObject, m_recordY, 0.3f).setEaseInCirc().setOnComplete((o =>
        {
            transform.parent = m_parent;
            m_render.sortingOrder = m_recordSortLayer;
            Vector3 position = new Vector3(character.position.x, m_recordY, 0f);
            gameObject.transform.position = position;
            m_collider2D.enabled = true;
            
            GlobalEvent.DispatchEvent("WeightDropDown");
            GlobalEvent.DispatchEvent("ShakeCamera");
        }));
    }
}
