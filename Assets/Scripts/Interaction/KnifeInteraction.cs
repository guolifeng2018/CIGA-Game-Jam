using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeInteraction : InteractionScript
{
    public enum EKnifeState
    {
        Slot = 0,
        Knife = 1,
        Screwdriver = 2,
        
        Max = 3,
    }

    private EKnifeState m_knifeState = EKnifeState.Slot;
    public Sprite m_knife;
    public Sprite m_slot;
    public Sprite m_screwdriver;
    
    public EKnifeState KnifeState
    {
        get { return m_knifeState; }
    }
    
    protected override void OnStart()
    {
        base.OnStart();
        
    }

    public override void DropDownItem(Transform character)
    {
        LeanTween.moveY(gameObject, m_recordY, 0.3f).setEaseOutBounce().setOnComplete((o =>
        {
            transform.parent = m_parent;
            m_render.sortingOrder = m_recordSortLayer;
            Vector3 position = new Vector3(character.position.x, m_recordY, 0f);
            gameObject.transform.position = position;
            m_collider2D.enabled = true;

            Character player = FindObjectOfType<Character>();
            player.PlayAudio("key_knife_drop");

            SwitchKnifeSlot();
        }));
    }

    private void SwitchKnifeSlot()
    {
        m_knifeState++;
        if (m_knifeState == EKnifeState.Max)
        {
            m_knifeState = EKnifeState.Slot;
        }

        m_render.flipX = false;
        switch (m_knifeState)
        {
            case EKnifeState.Slot:
                m_render.sprite = m_slot;
                break;
            case EKnifeState.Knife:
                m_render.sprite = m_knife;
                break;
            case EKnifeState.Screwdriver:
                m_render.sprite = m_screwdriver;
                break;
            default:
                m_render.sprite = m_slot;
                break;
        }
    }

    public override void PickUpItem(Transform parent)
    {
        base.PickUpItem(parent);

        if (m_knifeState == EKnifeState.Screwdriver)
        {
            m_render.flipX = true;
        }
    }
}
