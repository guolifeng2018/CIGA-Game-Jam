using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightsInteraction : InteractionScript
{
    private Rigidbody2D m_rigidBody2D;
    protected override void OnStart()
    {
        base.OnStart();
        m_rigidBody2D = GetComponent<Rigidbody2D>();
        if (m_rigidBody2D != null)
        {
            m_rigidBody2D.isKinematic = false;
        }
    }

    public override void PickUpItem(Transform parent)
    {
        base.PickUpItem(parent);

        if (m_rigidBody2D != null)
        {       
            m_rigidBody2D.isKinematic = true;
        }
    }

    public override void DropDownItem(Transform character)
    {
        if (m_rigidBody2D != null)
        {
            m_rigidBody2D.isKinematic = false;
        }
        LeanTween.moveY(gameObject, m_recordY, 0.3f).setEaseInCirc().setOnComplete((o =>
        {
            transform.parent = m_parent;
            m_render.sortingOrder = m_recordSortLayer;
            Vector3 position = new Vector3(character.position.x, m_recordY, 0f);
            gameObject.transform.position = position;
            m_collider2D.enabled = true;
            
            GlobalEvent.DispatchEvent("WeightDropDown");
            GlobalEvent.DispatchEvent("ShakeCamera");

            Character player = FindObjectOfType<Character>();
            player.PlayAudio("weight_drop_down");
        }));
    }
}
